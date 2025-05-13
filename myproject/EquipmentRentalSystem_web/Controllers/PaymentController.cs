using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using myproject_Library.Model;

namespace EquipmentRentalSystem_web.Controllers
{
    public class PaymentController : Controller
    {
        private readonly EquipmentDBContext _context;

        public PaymentController(EquipmentDBContext context)
        {
            _context = context;
        }

        [Authorize]
        public IActionResult Index(int transactionId)
        {
            var transaction = _context.RentalTransactions
                .Include(t => t.Equipment)
                .Include(y=> y.ReturnRecords)
                .FirstOrDefault(t => t.TransactionId == transactionId);

            if (transaction == null) return NotFound();
            return View(transaction); 
        }
  

        [HttpPost]
        public IActionResult Submit(IFormCollection form)
        {
            int transactionId = int.Parse(form["TransactionId"]);
            decimal amount = decimal.Parse(form["Amount"]);

            var transaction = _context.RentalTransactions
                .Include(t => t.ReturnRecords)
                .FirstOrDefault(t => t.TransactionId == transactionId);

            if (transaction == null)
                return NotFound();

            transaction.AmountPaid = (transaction.AmountPaid ?? 0) + amount;


            decimal covered = (transaction.AmountPaid ?? 0) + (transaction.Deposit ?? 0);
            decimal totalFee = transaction.TotalFee ?? 0;

            if (covered >= totalFee)
                transaction.PaymentStatusId = 1; // Paid
            else
                transaction.PaymentStatusId = 2; // unpaid

            _context.SaveChanges();

            return RedirectToAction("Index", new { transactionId });
        }





    }
}
