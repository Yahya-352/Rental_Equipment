﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using myproject_Library.Model;
using EquipmentRentalSystem_web.Hubs;
using Microsoft.EntityFrameworkCore;
using EquipmentRentalSystem_web.Classes;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace EquipmentRentalSystem_web.Controllers
{
    public class NotificationController : Controller
    {
        private readonly EquipmentDBContext _context;
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly NotificationService _notificationService;
        private readonly UserManager<User> _userManager;

        // Constructor with dependency injection
        public NotificationController(EquipmentDBContext context, IHubContext<NotificationHub> hubContext, NotificationService notificationService, UserManager<User> userManager)
        {
            _context = context;
            _hubContext = hubContext;
            _notificationService = notificationService;
            _userManager = userManager;
        }

        // Action for viewing notifications
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
                return RedirectToAction("Index", "Home");

            var userId = user.Id;

            HttpContext.Session.SetInt32("UserId", userId);

            var notifications = _context.Notifications
                .Where(n => n.UserId == userId)
                .ToList();

            return View(notifications);
        }

        // Action for checking new notifications
        [HttpGet]
        public async Task<IActionResult> CheckForNewNotification()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
                return Json(new { showNotification = false });

            var userId = user.Id;

            var notification = _context.Notifications
                .Where(n => n.UserId == userId)
                .OrderByDescending(n => n.NotificationId)
                .FirstOrDefault();

            if (notification != null)
            {
                await _hubContext.Clients
                    .Group($"user_{userId}")
                    .SendAsync("ReceiveNotification", new
                    {
                        notification.NotificationId,
                        notification.MessageContent,
                        notification.Type,
                        notification.Status,
                        notification.UserId
                    });

                return Json(new { showNotification = true });
            }

            return Json(new { showNotification = false });
        }

        // Action to manually create and insert a new notification
        [HttpPost]
        public async Task<IActionResult> CreateNotification([FromBody] Notification notification)
        {
            try
            {
                if (notification == null)
                {
                    return Json(new { success = false, message = "Notification is null" });
                }

                _context.Notifications.Add(notification);
                await _context.SaveChangesAsync();

                await _hubContext.Clients
                    .Group($"user_{notification.UserId}")
                    .SendAsync("ReceiveNotification", new
                    {
                        notification.NotificationId,
                        notification.MessageContent,
                        notification.Type,
                        notification.Status,
                        notification.UserId
                    });

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                // Optional: log ex.Message
                return Json(new { success = false, message = ex.Message });
            }
        }

        // Action to update notification status to "Read"
        [HttpPost]
        public async Task<IActionResult> MarkNotificationAsRead([FromBody] NotificationUpdateRequest request)
        {
            if (request == null || request.NotificationId == 0)
            {
                return Json(new { success = false, message = "Invalid request data." });
            }

            var notification = await _context.Notifications
                .FirstOrDefaultAsync(n => n.NotificationId == request.NotificationId);

            if (notification == null)
            {
                return Json(new { success = false, message = "Notification not found." });
            }

            if (notification.Status == "Unread")
            {
                notification.Status = "Read";
                await _context.SaveChangesAsync();

                // Notify clients that the status has changed
                await _hubContext.Clients
                    .Group($"user_{notification.UserId}")
                    .SendAsync("UpdateNotificationStatus", notification.NotificationId, "Read");

                return Json(new { success = true });
            }

            return Json(new { success = false, message = "Notification is already read or has an invalid status." });
        }



    }
}
