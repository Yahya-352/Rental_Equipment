using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using myproject_Library.Model;

namespace EquipmentRentalSystem_web.Controllers
{
    public class RentalTranscationController : Controller
    {

        private readonly EquipmentDBContext _context;

        public RentalTranscationController(EquipmentDBContext context)
        {
            _context = context;
        }


        [Authorize]
        public IActionResult Index()
        {
            var Rental_Transactions = _context.RentalTransactions
                .Include(y => y.Equipment)
                .Include(x => x.PaymentStatus)
                .ToList();

            var result = Rental_Transactions.Select(t => new
            {
                Transaction = t,
                IsReturned = _context.ReturnRecords.Any(r => r.TransactionId == t.TransactionId)
            }).ToList();

            return View(result);
        }

    }
}
