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
            return View(FeedBack);
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
