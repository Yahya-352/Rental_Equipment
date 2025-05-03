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

    

        public IActionResult Index()
        {
            IEnumerable<RentalTransaction> Rental_Transactions = _context.RentalTransactions
    .Include(y => y.Equipment)

    .Include(x => x.PaymentStatus)
    .ToList();

            return View(Rental_Transactions);
        }
    }
}
