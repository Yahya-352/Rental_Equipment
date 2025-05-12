using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using myproject_Library.Model;

namespace EquipmentRentalSystem_web.Controllers
{
    public class RentalRequestController : Controller
    {
        private readonly EquipmentDBContext _context;

        public RentalRequestController(EquipmentDBContext context) { 
            _context = context;
        }
        public async Task<IActionResult> Index(String SearchString, int? SearchStatus)
        {
            var requests = await _context.RentalRequests.Include(x => x.Equipment).Include(x => x.RequestStatus).ToListAsync();

            var statusList = await _context.RequestStatuses.Distinct()
            .ToListAsync();
            var equipmentList = await _context.Equipment.ToListAsync();
            ViewData["StatusList"] = statusList;
            if (!string.IsNullOrEmpty(SearchString))
            {
                requests = requests.Where(x => x.Equipment.EquipmentName.Contains(SearchString)).ToList();
            }
            if (SearchStatus != -1 && SearchStatus != null)
            {
                requests = requests.Where(x => x.RequestStatus.RequestStatusId == SearchStatus).ToList();
            }


            ViewData["SearchString"]= SearchString;
            ViewData["SearchStatus"] = SearchStatus;
            ViewData["EquipmentList"] = equipmentList;



            return View(requests);

        }
        [HttpGet]
        public JsonResult GetEquipmentRentalDates(int equipmentId)
        {
            var rentalRequests = _context.RentalRequests
                .Where(r => r.EquipmentId == equipmentId && r.RequestStatusId == 2)
                .Include(r => r.Equipment)
                .ToList();

            var rentalDates = rentalRequests.Select(r =>
            {
                var transaction = _context.RentalTransactions
                    .FirstOrDefault(t => t.RequestId == r.RequestId);

                DateTime startDate = transaction?.RentalStartDate ?? r.StartDate ?? DateTime.MinValue;
                DateTime endDate = transaction?.RentalReturnDate ?? r.ReturnDate ?? DateTime.MinValue;

                return new
                {
                    title = r.Equipment?.EquipmentName ?? "Unknown Equipment",
                    start = startDate.ToString("yyyy-MM-dd"),
                    end = endDate.ToString("yyyy-MM-dd")
                };
            }).ToList();

            return Json(rentalDates);
        }

        [HttpGet]
        public IActionResult CreateRequest()
        {
            ViewData["equipments"] = _context.Equipment.ToList(); // ✅ This is needed
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RentalRequest request)
        {
            request.RequestStatusId = 1; // pending
            request.UserId = 1; // hardcoded for now

            if (!ModelState.IsValid || request.StartDate == null || request.ReturnDate == null)
            {
                TempData["Error"] = "Invalid data. Please complete all required fields.";
                ViewData["equipments"] = _context.Equipment.ToList();
                return View("CreateRequest", request);
            }
            if (request.ReturnDate <= request.StartDate)
            {
                TempData["Error"] = "Return date must be after start date.";
                ViewData["equipments"] = _context.Equipment.ToList();
                return View("CreateRequest", request);
            }


            // Conflict check
            if (await HasDateConflict(request.RequestId, request.EquipmentId ?? 0, request.StartDate.Value, request.ReturnDate.Value))
            {
                TempData["Error"] = "Date conflict detected. Please select a different rental period.";
                ViewData["equipments"] = _context.Equipment.ToList();
                return View("CreateRequest", request);
            }

            _context.RentalRequests.Add(request);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Rental request created successfully.";
            return RedirectToAction("MyRequests");
        }

        private async Task<bool> HasDateConflict(int currentRequestId, int equipmentId, DateTime start, DateTime end)
        {
            var approvedRequests = await _context.RentalRequests
                .Where(r => r.EquipmentId == equipmentId &&
                            r.RequestId != currentRequestId &&
                            r.RequestStatus.RequestStatusName == "Approved")
                .ToListAsync();

            foreach (var r in approvedRequests)
            {
                var transaction = await _context.RentalTransactions
                    .FirstOrDefaultAsync(t => t.RequestId == r.RequestId);

                DateTime checkStart = transaction?.RentalStartDate ?? r.StartDate ?? DateTime.MinValue;
                DateTime checkEnd = transaction?.RentalReturnDate ?? r.ReturnDate ?? DateTime.MinValue;

                if (checkStart <= end && checkEnd >= start)
                {
                    return true;
                }
            }

            return false;
        }



        public async Task<IActionResult> UpdateRequest(int requestid)
        {
            var request = _context.RentalRequests.Find(requestid);
            var equipments = await _context.Equipment.ToListAsync();

            var statusList = _context.RequestStatuses.Distinct()
            .ToList();

            ViewData["StatusList"] = statusList;
            ViewData["equipments"] = equipments;
            return View(request);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(RentalRequest request)
        {
            if (!ModelState.IsValid || request.StartDate == null || request.ReturnDate == null)
            {
                TempData["Error"] = "Invalid input. Please check your fields.";
                ViewData["equipments"] = await _context.Equipment.ToListAsync();
                return View("UpdateRequest", request);
            }

            if (request.ReturnDate <= request.StartDate)
            {
                TempData["Error"] = "Return date must be after start date.";
                ViewData["equipments"] = await _context.Equipment.ToListAsync();
                return View("UpdateRequest", request);
            }

            // Optional: recheck conflict only if still pending
            var status = await _context.RequestStatuses.FindAsync(request.RequestStatusId);
            if (status?.RequestStatusName == "Pending")
            {
                if (await HasDateConflict(request.RequestId, request.EquipmentId ?? 0, request.StartDate.Value, request.ReturnDate.Value))
                {
                    TempData["Error"] = "Date conflict detected. Please choose another period.";
                    ViewData["equipments"] = await _context.Equipment.ToListAsync();
                    return View("UpdateRequest", request);
                }
            }

            _context.RentalRequests.Update(request);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Rental request updated successfully.";
            return RedirectToAction("MyRequests");
        }




        [HttpPost]
        public async Task<IActionResult> UpdateStatus(int requestId, string status)
        {
            var request = await _context.RentalRequests.FindAsync(requestId);
            if (request == null) return NotFound();

            var statusEntity = await _context.RequestStatuses
                .FirstOrDefaultAsync(s => s.RequestStatusName.ToLower() == status.ToLower());

            if (statusEntity == null) return BadRequest("Invalid status");

            // If trying to approve, check for conflicts
            if (statusEntity.RequestStatusName == "Approved")
            {
                if (await HasDateConflict(requestId))
                {
                    TempData["Error"] = "Date conflict detected. Cannot approve.";
                    return RedirectToAction("Index");
                }
            }

            request.RequestStatusId = statusEntity.RequestStatusId;

            _context.RentalRequests.Update(request);
            await _context.SaveChangesAsync();
            if (statusEntity.RequestStatusName == "Cancelled")
            {
                return RedirectToAction("MyRequests");
            }


            return RedirectToAction("Index");
        }

        private async Task<bool> HasDateConflict(int requestId)
        {
            var request = await _context.RentalRequests
                .FirstOrDefaultAsync(r => r.RequestId == requestId);

            if (request == null || request.StartDate == null || request.ReturnDate == null)
                return false;

            var approvedRequests = await _context.RentalRequests
                .Where(r => r.EquipmentId == request.EquipmentId &&
                            r.RequestId != requestId &&
                            r.RequestStatus.RequestStatusName == "Approved")
                .ToListAsync();

            foreach (var r in approvedRequests)
            {
                var transaction = await _context.RentalTransactions
                    .FirstOrDefaultAsync(t => t.RequestId == r.RequestId);

                DateTime checkStart = transaction?.RentalStartDate ?? r.StartDate.Value;
                DateTime checkEnd = transaction?.RentalReturnDate ?? r.ReturnDate.Value;

                DateTime currentStart = request.StartDate.Value;
                DateTime currentEnd = request.ReturnDate.Value;

                if (checkStart <= currentEnd && checkEnd >= currentStart)
                {
                    return true; // Overlapping found
                }
            }

            return false; // No conflict
        }

        [HttpGet]
        public IActionResult MyRequests()
        {
            int userId = 1; // TEMP: Replace with actual user authentication later

            var userRequests = _context.RentalRequests
                .Include(r => r.Equipment)
                .Include(r => r.RequestStatus)
                .Where(r => r.UserId == userId)
                .ToList();

            return View(userRequests);
        }















    }
}
