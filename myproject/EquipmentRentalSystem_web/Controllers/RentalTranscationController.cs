using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using myproject_Library.Model;

namespace EquipmentRentalSystem_web.Controllers
{
    public class RentalTranscationController : Controller
    {

        private readonly EquipmentDBContext _context;
        private readonly UserManager<User> _userManager;


        public RentalTranscationController(EquipmentDBContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;

        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult>  MyTranscations()
        {

            var user = await _userManager.GetUserAsync(User);
            var rentalTransactions = await _context.RentalTransactions
            .Include(y => y.Equipment)
            .Include(x => x.PaymentStatus)
             .Include(p => p.ReturnRecords)

            .Include(t => t.Documents)
            .Include(t => t.Request)
                .ThenInclude(Z => Z.User)
            .Where(x => x.Request.UserId == user.Id) 
            .ToListAsync();

            var viewModel = rentalTransactions.Select(t => new
            {
                Transaction = t,
                IsReturned = t.ReturnRecords.Any(r => r.TransactionId == t.TransactionId),
                Documents = t.Documents
            });
            return View(viewModel);

        }

        [HttpGet]
        [Authorize(Policy = "RequireDashboardAccess")]
        public IActionResult Index(string equipmentSearch, string requestSearch)
        {
            var rentalTransactions = _context.RentalTransactions
                .Include(y => y.Equipment)
                .Include(x => x.PaymentStatus)
                .Include(t => t.Documents)
                .Include(t => t.Request)
                .AsQueryable();

            if (!string.IsNullOrEmpty(equipmentSearch))
            {
                rentalTransactions = rentalTransactions
                    .Where(t => t.Equipment.EquipmentName.Contains(equipmentSearch));
            }

            if (!string.IsNullOrEmpty(requestSearch) && int.TryParse(requestSearch, out int requestId))
            {
                rentalTransactions = rentalTransactions
                    .Where(t => t.RequestId == requestId);
            }

            var result = rentalTransactions
                .ToList()
                .Select(t => new
                {
                    Transaction = t,
                    IsReturned = _context.ReturnRecords.Any(r => r.TransactionId == t.TransactionId),
                    Documents = t.Documents.ToList()
                }).ToList();

            ViewData["EquipmentSearch"] = equipmentSearch;
            ViewData["RequestSearch"] = requestSearch;

            return View(result);
        }

        [HttpGet]
        [Authorize(Policy = "RequireDashboardAccess")]
        public IActionResult CreateFromRequest(int requestId)
        {
            var request = _context.RentalRequests
                .Include(r => r.Equipment)
                .FirstOrDefault(r => r.RequestId == requestId && r.RequestStatus.RequestStatusName == "Approved");

            if (request == null)
            {
                TempData["Error"] = "Request not found or not approved.";
                return RedirectToAction("Index", "RentalRequest");
            }

            bool transactionExists = _context.RentalTransactions.Any(t => t.RequestId == requestId);
            if (transactionExists)
            {
                TempData["Error"] = "A transaction already exists for this request.";
                return RedirectToAction("Index", "RentalRequest");
            }

            int rentalPeriod = (request.ReturnDate - request.StartDate)?.Days ?? 1;
            decimal rentalPrice = request.Equipment?.RentalPrice ?? 0;
            ViewData["RentalPrice"] = rentalPrice;

            decimal rentalFee = rentalPrice * rentalPeriod;

            var transaction = new RentalTransaction
            {
                RequestId = request.RequestId,
                EquipmentId = request.EquipmentId,
                RentalStartDate = request.StartDate,
                RentalReturnDate = request.ReturnDate,
                RentalPeriod = rentalPeriod,
                RentalFee = rentalFee
            };

            ViewData["EquipmentName"] = request.Equipment.EquipmentName;
            ViewBag.PaymentStatusList = new SelectList(_context.PaymentStatuses, "PaymentStatusId", "PaymentStatusName");

            return View(transaction);
        }

        [HttpPost]
        [Authorize(Policy = "RequireDashboardAccess")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateFromRequest(RentalTransaction transaction, IFormFile? RentalDocument)
        {
            var user = await _userManager.GetUserAsync(User);

            // Step 1: Check for date conflicts
            if (await HasDateConflict(
                transaction.EquipmentId ?? 0,
                transaction.RequestId,
                transaction.RentalStartDate ?? DateTime.MinValue,
                transaction.RentalReturnDate ?? DateTime.MaxValue))
            {
                TempData["Error"] = "Conflict with another booking during the selected dates.";
                await LoadFormData(transaction); // Populate dropdowns and equipment info again
                return View(transaction);
            }
            if (transaction.RentalStartDate >= transaction.RentalReturnDate) {
                TempData["Error"] = "return date must be after start date.";
                await LoadFormData(transaction); // Populate dropdowns and equipment info again
                return View(transaction);
            }

            // Step 2: Proceed if model is valid
            if (ModelState.IsValid)
            {
                // Save transaction first to get TransactionId
                _context.RentalTransactions.Add(transaction);
                await _context.SaveChangesAsync();

                // Step 3: If a file is uploaded, save it
                if (RentalDocument != null && RentalDocument.Length > 0)
                {
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "documents");
                    Directory.CreateDirectory(uploadsFolder); // Ensure the folder exists

                    var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(RentalDocument.FileName);
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await RentalDocument.CopyToAsync(stream);
                    }

                    var document = new Document
                    {
                        DocumentName = Path.GetFileNameWithoutExtension(RentalDocument.FileName),
                        UploadDate = DateTime.Now,
                        FileType = Path.GetExtension(RentalDocument.FileName),
                        StoragePath = Path.Combine("documents", uniqueFileName),
                        UserId = user.Id, 
                        TransactionId = transaction.TransactionId
                    };

                    _context.Documents.Add(document);
                    await _context.SaveChangesAsync();
                }

                TempData["Success"] = "Transaction created successfully.";
                return RedirectToAction("Index", "RentalRequest");
            }

            // Step 4: If invalid model, reload view
            await LoadFormData(transaction);
            return View(transaction);
        }

        // Helper to reload dropdowns and equipment info
        private async Task LoadFormData(RentalTransaction transaction)
        {
            ViewBag.PaymentStatusList = new SelectList(_context.PaymentStatuses, "PaymentStatusId", "PaymentStatusName", transaction.PaymentStatusId);

            ViewData["EquipmentName"] = await _context.Equipment
                .Where(e => e.EquipmentId == transaction.EquipmentId)
                .Select(e => e.EquipmentName)
                .FirstOrDefaultAsync();

            ViewData["RentalPrice"] = await _context.Equipment
                .Where(e => e.EquipmentId == transaction.EquipmentId)
                .Select(e => e.RentalPrice)
                .FirstOrDefaultAsync();
        }



        private async Task<bool> HasDateConflict(int equipmentId, int? requestIdToExclude, DateTime start, DateTime end)
        {
            var approvedRequests = await _context.RentalRequests
                .Where(r => r.EquipmentId == equipmentId &&
                            r.RequestStatus.RequestStatusName == "Approved" &&
                            (requestIdToExclude == null || r.RequestId != requestIdToExclude))
                .ToListAsync();

            foreach (var r in approvedRequests)
            {
                var transaction = await _context.RentalTransactions
                    .FirstOrDefaultAsync(t => t.RequestId == r.RequestId);

                DateTime checkStart = transaction?.RentalStartDate ?? r.StartDate ?? DateTime.MinValue;
                DateTime checkEnd = transaction?.RentalReturnDate ?? r.ReturnDate ?? DateTime.MaxValue;

                if (checkStart <= end && checkEnd >= start)
                {
                    return true;
                }
            }

            return false;
        }

        [HttpGet]
        [Authorize(Policy = "RequireDashboardAccess")]
        public async Task<IActionResult> UpdateTransaction(int id)
        {
            var transaction = await _context.RentalTransactions
                .Include(t => t.Equipment)
                .FirstOrDefaultAsync(t => t.TransactionId == id);

            if (transaction == null)
            {
                TempData["Error"] = "Transaction not found.";
                return RedirectToAction("Index", "RentalRequest");
            }

            ViewBag.PaymentStatusList = new SelectList(_context.PaymentStatuses, "PaymentStatusId", "PaymentStatusName", transaction.PaymentStatusId);
            ViewData["EquipmentName"] = transaction.Equipment?.EquipmentName ?? "Unknown";
            ViewData["RentalPrice"] = transaction.Equipment?.RentalPrice ?? 0;

            return View("UpdateTransaction", transaction);
        }

        [HttpPost]
        [Authorize(Policy = "RequireDashboardAccess")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateTransaction(RentalTransaction transaction)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.PaymentStatusList = new SelectList(_context.PaymentStatuses, "PaymentStatusId", "PaymentStatusName", transaction.PaymentStatusId);
                ViewData["EquipmentName"] = _context.Equipment
                    .Where(e => e.EquipmentId == transaction.EquipmentId)
                    .Select(e => e.EquipmentName)
                    .FirstOrDefault();
                ViewData["RentalPrice"] = _context.Equipment
                    .Where(e => e.EquipmentId == transaction.EquipmentId)
                    .Select(e => e.RentalPrice)
                    .FirstOrDefault();
                return View("UpdateTransaction", transaction);
            }
            if (await HasDateConflict(
                transaction.EquipmentId ?? 0,
                transaction.RequestId,
                transaction.RentalStartDate ?? DateTime.MinValue,
                transaction.RentalReturnDate ?? DateTime.MaxValue))
            {
                TempData["Error"] = "Conflict with another booking during the selected dates.";
                ViewBag.PaymentStatusList = new SelectList(_context.PaymentStatuses, "PaymentStatusId", "PaymentStatusName", transaction.PaymentStatusId);
                ViewData["EquipmentName"] = _context.Equipment
                    .Where(e => e.EquipmentId == transaction.EquipmentId)
                    .Select(e => e.EquipmentName)
                    .FirstOrDefault();
                ViewData["RentalPrice"] = _context.Equipment
                    .Where(e => e.EquipmentId == transaction.EquipmentId)
                    .Select(e => e.RentalPrice)
                    .FirstOrDefault();
                return View("UpdateTransaction", transaction);
            }
            if (transaction.RentalStartDate >= transaction.RentalReturnDate)
            {
                TempData["Error"] = "return date must be after start date.";
                await LoadFormData(transaction); // Populate dropdowns and equipment info again
                return View(transaction);
            }

            var existing = await _context.RentalTransactions.FindAsync(transaction.TransactionId);
            if (existing == null)
            {
                TempData["Error"] = "Transaction not found.";
                return RedirectToAction("Index", "RentalTranscation");
            }

            existing.RentalStartDate = transaction.RentalStartDate;
            existing.RentalReturnDate = transaction.RentalReturnDate;
            existing.RentalPeriod = transaction.RentalPeriod;
            existing.RentalFee = transaction.RentalFee;
            existing.Deposit = transaction.Deposit;
            existing.AmountPaid = transaction.AmountPaid;
            existing.TotalFee = transaction.TotalFee;
            existing.PaymentStatusId = transaction.PaymentStatusId;

            await _context.SaveChangesAsync();

            TempData["Success"] = "Transaction updated successfully.";
            return RedirectToAction("Index", "RentalTranscation");
        }
    }
}
