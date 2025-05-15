using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using myproject_Library.Model;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace EquipmentRental.web.Controllers
{
    public class EquipmentController : Controller
    {
        private readonly EquipmentDBContext _context;

        public EquipmentController(EquipmentDBContext context)
        {
            _context = context;
        }

        // GET: /Equipment/
        public async Task<IActionResult> Index(string searchString, int? categoryId)
        {
            // Get equipment with related entities
            var equipmentQuery = _context.Equipment
                .Include(e => e.Category)
                .Include(e => e.AvailabilityStatus)
                .Include(e => e.Condition)
                .AsQueryable();

            // Apply search filter if provided
            if (!string.IsNullOrEmpty(searchString))
            {
                equipmentQuery = equipmentQuery.Where(e => 
                    (e.EquipmentName != null && e.EquipmentName.Contains(searchString)) || 
                    (e.Description != null && e.Description.Contains(searchString)));
            }

            // Apply category filter if provided
            if (categoryId.HasValue && categoryId.Value > 0)
            {
                equipmentQuery = equipmentQuery.Where(e => e.CategoryId == categoryId);
            }

            // Create a list of SelectListItem for categories
            var categories = await _context.Categories
                .OrderBy(c => c.CategoryName)
                .Select(c => new SelectListItem
                {
                    Value = c.CategoryId.ToString(),
                    Text = c.CategoryName ?? "Unknown",
                    Selected = categoryId.HasValue && c.CategoryId == categoryId.Value
                })
                .ToListAsync();
            
            // Add 'All Categories' option at the beginning
            categories.Insert(0, new SelectListItem
            {
                Value = "",
                Text = "All Categories",
                Selected = !categoryId.HasValue || categoryId.Value == 0
            });
            
            // Pass data to view
            ViewBag.Categories = categories;
            ViewBag.SearchString = searchString;
            ViewBag.SelectedCategoryId = categoryId;
            
            // Get the equipment list
            var equipment = await equipmentQuery.ToListAsync();
            
            // For debugging - check if we have any data
            if (!equipment.Any())
            {
                ViewBag.NoData = true;
            }

            return View(equipment);
        }

        // GET: /Equipment/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var equipment = await _context.Equipment
                .Include(e => e.RentalTransactions)
                .Include(e => e.Category)
                .Include(e => e.AvailabilityStatus)
                .Include(e => e.Condition)
                .FirstOrDefaultAsync(m => m.EquipmentId == id);

            if (equipment == null) return NotFound();

            var feedbacks = new List<myproject_Library.Model.Feedback>();

            if (equipment.RentalTransactions != null && equipment.RentalTransactions.Any())
            {
                var transactionIds = equipment.RentalTransactions
                    .Select(t => t.TransactionId)
                    .ToList();

                feedbacks = _context.Feedbacks
                    .Include(z=> z.User)
                    .Where(z => transactionIds.Contains((int)z.TransactionId))
                    .Where(s=> s.IsVisible ==  true)
                    .ToList();
            }

            ViewData["feedback"] = feedbacks;
            return View(equipment);
        }

        // GET: /Equipment/Create
        [Authorize(Roles = "Administrator,Manager")]
        public async Task<IActionResult> Create()
        {
            // Load dropdown data
            await LoadDropdownData();
            return View();
        }

        // POST: /Equipment/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,Manager")]
        public async Task<IActionResult> Create(Equipment equipment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(equipment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // If we got this far, something failed, reload dropdown data
            await LoadDropdownData();
            return View(equipment);
        }

        // GET: /Equipment/Edit/5
        [Authorize(Roles = "Administrator,Manager")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var equipment = await _context.Equipment.FindAsync(id);
            if (equipment == null) return NotFound();

            // Load dropdown data with pre-selected values
            await LoadDropdownData();
            
            // Manually set selected values for dropdowns
            if (equipment.CategoryId.HasValue)
            {
                foreach (var item in ViewBag.Categories)
                {
                    if (item.Value == equipment.CategoryId.ToString())
                    {
                        item.Selected = true;
                        break;
                    }
                }
            }
            
            if (equipment.AvailabilityStatusId.HasValue)
            {
                foreach (var item in ViewBag.AvailabilityStatuses)
                {
                    if (item.Value == equipment.AvailabilityStatusId.ToString())
                    {
                        item.Selected = true;
                        break;
                    }
                }
            }
            
            if (equipment.ConditionId.HasValue)
            {
                foreach (var item in ViewBag.Conditions)
                {
                    if (item.Value == equipment.ConditionId.ToString())
                    {
                        item.Selected = true;
                        break;
                    }
                }
            }
            
            return View(equipment);
        }

        // POST: /Equipment/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,Manager")]
        public async Task<IActionResult> Edit(int id, Equipment equipment)
        {
            if (id != equipment.EquipmentId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(equipment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EquipmentExists(equipment.EquipmentId))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }

            // If we got this far, something failed, reload dropdown data
            await LoadDropdownData();
            return View(equipment);
        }

        // GET: /Equipment/Delete/5
        [Authorize(Roles = "Administrator,Manager")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var equipment = await _context.Equipment
                .Include(e => e.Category)
                .Include(e => e.AvailabilityStatus)
                .Include(e => e.Condition)
                .FirstOrDefaultAsync(m => m.EquipmentId == id);

            if (equipment == null) return NotFound();

            return View(equipment);
        }

        // POST: /Equipment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,Manager")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var equipment = await _context.Equipment.FindAsync(id);
            if (equipment == null) return NotFound();

            _context.Equipment.Remove(equipment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EquipmentExists(int id)
        {
            return _context.Equipment.Any(e => e.EquipmentId == id);
        }

        private async Task LoadDropdownData()
        {
            // Create SelectList for Categories
            ViewBag.Categories = await _context.Categories
                .Select(c => new SelectListItem
                {
                    Value = c.CategoryId.ToString(),
                    Text = c.CategoryName ?? "Unknown"
                })
                .ToListAsync();
            
            // Create SelectList for AvailabilityStatuses
            ViewBag.AvailabilityStatuses = await _context.EquipmentAvailabilities
                .Select(a => new SelectListItem
                {
                    Value = a.AvailabilityStatusId.ToString(),
                    Text = a.AvailabilityStatusName ?? "Unknown"
                })
                .ToListAsync();
            
            // Create SelectList for Conditions
            ViewBag.Conditions = await _context.EquipmentConditions
                .Select(c => new SelectListItem
                {
                    Value = c.ConditionId.ToString(),
                    Text = c.ConditionName ?? "Unknown"
                })
                .ToListAsync();
            
            // Add empty option at the beginning of each list
            ViewBag.Categories.Insert(0, new SelectListItem { Value = "", Text = "-- Select Category --" });
            ViewBag.AvailabilityStatuses.Insert(0, new SelectListItem { Value = "", Text = "-- Select Status --" });
            ViewBag.Conditions.Insert(0, new SelectListItem { Value = "", Text = "-- Select Condition --" });
        }
    }
}
