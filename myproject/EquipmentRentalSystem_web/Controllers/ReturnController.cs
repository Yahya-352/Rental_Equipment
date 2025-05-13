using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using myproject_Library.Model;

namespace EquipmentRentalSystem_web.Controllers
{
    public class ReturnController : Controller
    {
        private readonly EquipmentDBContext _context;
        private readonly UserManager<User> _userManager;

        public ReturnController(EquipmentDBContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;

        }

        public async Task<IActionResult> MyReturns(){
            var user = await _userManager.GetUserAsync(User);

            var returnedItems = await _context.ReturnRecords
            .Include(r => r.Transaction)
                .ThenInclude(t => t.Equipment)
            .Include(r => r.Transaction)
                .ThenInclude(t => t.PaymentStatus)
            .Include(r => r.Condition)
            .Where(f => f.Transaction.Request.UserId == user.Id)
            .OrderByDescending(r => r.ActualReturnDate)
            .ToListAsync();

            ViewData["Title"] = "My Returned Items";
            ViewData["TotalReturns"] = returnedItems.Count;
            return View(returnedItems);
        }

        [Authorize]
        public IActionResult Index()
        {
            var returnedItems = _context.ReturnRecords
              .Include(r => r.Transaction)
                  .ThenInclude(t => t.Equipment)
                  .Include(z => z.Condition)
              .ToList();


            return View(returnedItems);
        }
        [HttpPost]
        public IActionResult ShowPayment(int? selectedReturnId)
        {
            if (selectedReturnId == null)
            {
                TempData["Error"] = "Please select a return record.";
                return RedirectToAction("Index");
            }

            // Redirect to Payment controller
            return RedirectToAction("Index", "Payment", new
            {
                transactionId = selectedReturnId.Value
            });
        }



        [HttpPost]
        public IActionResult ProcessReturn(int transactionId)
        {
            if ((_context.ReturnRecords.Any(x => x.TransactionId == transactionId)))
            {

                return RedirectToAction("Index");

            }
            var transaction = _context.RentalTransactions
                .Include(t => t.Equipment)
                .Include(t => t.PaymentStatus)
                .FirstOrDefault(t => t.TransactionId == transactionId);

            if (transaction == null)
                return NotFound();

            var plan_date = transaction.RentalReturnDate.Value;
            var start_dat = transaction.RentalStartDate.Value;


            int lateDays = Math.Max(0, (DateTime.Now - transaction.RentalReturnDate.Value).Days);
            decimal equipmentCost = transaction.Equipment?.Cost ?? 0;
            int EarlyDays = Convert.ToInt32((plan_date.Date - start_dat.Date).Days);

            decimal earlyFee = Convert.ToDecimal(
                    (EarlyDays * transaction.RentalFee)); ;
            decimal lateFee = +Convert.ToDecimal((lateDays * transaction.RentalFee)
                    + (lateDays * (transaction.RentalFee / 100) * 20));
            decimal total = earlyFee + lateFee;

            ViewBag.ReturnDay = DateTime.Now.ToString("yyyy-MM-dd");
            ViewBag.LateDays = lateDays;
            ViewBag.EarlyDays = EarlyDays;

            ViewBag.EarlyFees = earlyFee;
            ViewBag.LateFees = lateFee;

            ViewBag.TotalFee = total;

            return View("Create_Return", transaction);
        }

        [HttpPost]
        public IActionResult SubmitReturn(IFormCollection form)
        {
            int transactionId = int.Parse(form["TransactionId"]);
            DateTime returnDate = DateTime.Parse(form["ReturnDate"]);
            int lateDays = int.Parse(form["LateDays"]);
            decimal lateFee = decimal.Parse(form["CalculatedLateFee"]);
            decimal totalFee = decimal.Parse(form["TotalFee"]);
            string conditionRaw = form["Condition"];

            string[] conditionParts = conditionRaw.Split('-');
            string conditionName = conditionParts[0]; 
            int conditionId = int.Parse(conditionParts[1]);

            var transaction = _context.RentalTransactions
                .Include(z=> z.Equipment)
       .FirstOrDefault(t => t.TransactionId == transactionId);
            var returnRecord = new ReturnRecord
            {
                TransactionId = transactionId,
                ActualReturnDate = returnDate,
                LateReturnDays = lateDays,
                LateReturnFees = lateFee,
                ConditionId = conditionId,
            };
            _context.ReturnRecords.Add(returnRecord);
            transaction.TotalFee = totalFee;

            var feedback = new Feedback
            {
                UserId = 7,
                TransactionId = transactionId,
                Rating = int.TryParse(form["rate"], out var r) ? r : 0,
                Date = returnDate.Date,           
                Time = DateTime.Now.TimeOfDay,
                CommentText = form["CommentText"].ToString() ?? null
            };
            transaction.Equipment.AvailabilityStatusId = conditionId;
            _context.Feedbacks.Add(feedback);

            _context.SaveChanges();
            return RedirectToAction("Index", "Payment", new
            {
                transactionId = transaction.TransactionId

            });

        }



        /*
            public IActionResult Index()
        {
            return View();
        }
        */
    }
}
