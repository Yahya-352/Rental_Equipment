using EquipmentRentalSystem_web.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using myproject_Library.Model;

public class NotificationService
{
    private readonly IHubContext<NotificationHub> _hubContext;
    private readonly EquipmentDBContext _dbContext;

    // Constructor for injecting dependencies
    public NotificationService(IHubContext<NotificationHub> hubContext, EquipmentDBContext dbContext)
    {
        _hubContext = hubContext;
        _dbContext = dbContext;
    }

    // Method that accepts individual notification properties
    public async Task InsertNotificationAndNotifyAsync(string messageContent, string type, string status, int userId)
    {
        var notification = new Notification
        {
            MessageContent = messageContent,
            Type = type,
            Status = status,
            UserId = userId
        };

        _dbContext.Notifications.Add(notification);
        await _dbContext.SaveChangesAsync();

        await _hubContext.Clients.Group($"user_{userId}")
            .SendAsync("ReceiveNotification", new
            {
                notification.NotificationId,
                notification.MessageContent,
                notification.Type,
                notification.Status,
                notification.UserId
            });
    }

}
