using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using myproject_Library.Model;

namespace EquipmentRentalSystem_web.Controllers
{
    public class ReturnController : Controller
    {
        private readonly EquipmentDBContext _context;

        public ReturnController(EquipmentDBContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var returnedItems = _context.ReturnRecords
                .Include(r => r.Transaction)
                    .ThenInclude(t => t.Equipment)
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
