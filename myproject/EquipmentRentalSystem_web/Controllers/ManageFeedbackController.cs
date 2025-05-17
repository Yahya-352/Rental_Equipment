using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using myproject_Library.Model;
using System.Collections.ObjectModel;

namespace EquipmentRentalSystem_web.Controllers
{
    public class ManageFeedbackController : Controller
    {
        private readonly EquipmentDBContext _context;

        public ManageFeedbackController(EquipmentDBContext context)
        {
            _context = context;
        }
        [Authorize]

        public IActionResult Index()
        {
            var FeedBack = _context.Feedbacks
                .Include(x=> x.Transaction)
                    .ThenInclude(p => p.ReturnRecords)
                  .Include(x => x.Transaction)
                      .ThenInclude(z => z.Equipment)

                .Include(y=> y.User)
                .ToList();
            ViewData["Users"] = _context.Users.ToList();
            ViewData["equips"] = _context.Equipment.ToList();
            return View(FeedBack);
        }

        [HttpGet]
        public IActionResult FilterFeeds(
            int? userId = null,
            int? equipmentId = null,
            string equipmentSearch = null,
            int? returnId = null,
            int? transactionId = null)
        {
            var query = _context.Feedbacks
                .Include(x => x.Transaction)
                    .ThenInclude(p => p.ReturnRecords)
                .Include(x => x.Transaction)
                    .ThenInclude(z => z.Equipment)
                .Include(y => y.User)
                .AsQueryable();

            if (userId.HasValue)
            {
                query = query.Where(f => f.UserId == userId.Value);
            }
            if (equipmentId.HasValue)
            {
                query = query.Where(f => f.Transaction.EquipmentId == equipmentId.Value);
            }

            if (!string.IsNullOrEmpty(equipmentSearch))
            {
                query = query.Where(f => f.Transaction.Equipment.EquipmentName.Contains(equipmentSearch));
            }
            if (returnId.HasValue)
            {
                query = query.Where(f => f.Transaction.ReturnRecords.Any(r => r.ReturnId == returnId.Value));
            }
            if (transactionId.HasValue)
            {
                query = query.Where(f => f.TransactionId == transactionId.Value);
            }

            var filteredFeedback = query.ToList();
            ViewData["Users"] = _context.Users.ToList();
            ViewData["equips"] = _context.Equipment.ToList();

            return View("Index", filteredFeedback);
        }
        [HttpPost]
        public IActionResult ProcessFeedbacks(IFormCollection form)
        {

            var keys = form["FeedbackId"].ToArray();
            var values = form["IsVisible"].ToArray();

           for(int i = 0; i < keys.Length; i++)
            {
                int tempkey = int.Parse(keys[i]);
                bool tempVal = bool.Parse(values[i]);

                var feedback = _context.Feedbacks.Find(tempkey);

                feedback.IsVisible = tempVal;

            }

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

    
}
}
