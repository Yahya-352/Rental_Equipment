using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using myproject_Library.Model;
using EquipmentRentalSystem_web.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace EquipmentRentalSystem_web.Controllers
{
    [Authorize(Policy = "RequireDashboardAccess")]
    public class DashboardController : Controller
    {
        private readonly EquipmentDBContext _context;

        public DashboardController(EquipmentDBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var dashboardViewModel = new DashboardViewModel
            {
                PendingRequestsCount = 0,
                ApprovedRequestsCount = 0,
                CompletedRequestsCount = 0,
                RejectedRequestsCount = 0,
                CancelledRequestsCount = 0,
                OverdueRequestsCount = 0,
                DamagedEquipmentCount = 0,
                RequestsByCategoryData = new List<CategoryDataPoint>(),
                FinancialSummary = new FinancialSummaryData(),
                EquipmentStatusData = new EquipmentStatusData(),
                TopRentedEquipment = new List<EquipmentRentalData>(),
                RecentTransactions = new List<RecentTransactionData>()
            };

            // Retrieve and populate the dashboard metrics
            await PopulateDashboardMetrics(dashboardViewModel);

            ViewBag.Categories = await _context.Categories.ToListAsync();

            return View(dashboardViewModel);
        }

        private async Task PopulateDashboardMetrics(DashboardViewModel model)
        {
            // Get current date for comparisons
            var today = DateTime.Today;

            // Request status counts
            model.PendingRequestsCount = await _context.RentalRequests
                .Where(r => r.RequestStatusId == 1) // 1 is Pending status
                .CountAsync();

            model.ApprovedRequestsCount = await _context.RentalRequests
                .Where(r => r.RequestStatusId == 2) // 2 is Approved status
                .CountAsync();

            model.RejectedRequestsCount = await _context.RentalRequests
                .Where(r => r.RequestStatusId == 3) // 3 is Rejected status
                .CountAsync();

            model.CancelledRequestsCount = await _context.RentalRequests
                .Where(r => r.RequestStatusId == 4) // 4 is Cancelled status
                .CountAsync();

            model.CompletedRequestsCount = await _context.RentalRequests
                .Where(r => r.RequestStatusId == 5) // 5 is Completed status
                .CountAsync();

            // Overdue Requests (transactions with return date passed but not returned)
            model.OverdueRequestsCount = await _context.RentalTransactions
                .Where(t => t.RentalReturnDate < today && !_context.ReturnRecords.Any(r => r.TransactionId == t.TransactionId))
                .CountAsync();

            // Damaged Equipment
            model.DamagedEquipmentCount = await _context.Equipment
                .Where(e => e.ConditionId == 3) // 3 is Damaged condition
                .CountAsync();

            // Lost Equipment
            model.LostEquipmentCount = await _context.Equipment
                .Where(e => e.ConditionId == 2) // 2 is Lost condition
                .CountAsync();

            // 3. Requests by Category
            var requestsByCategory = await _context.RentalRequests
                .Join(_context.Equipment, req => req.EquipmentId, eq => eq.EquipmentId, (req, eq) => new { req, eq })
                .GroupBy(x => x.eq.CategoryId)
                .Select(g => new { CategoryId = g.Key, Count = g.Count() })
                .ToListAsync();

            foreach (var item in requestsByCategory)
            {
                var category = await _context.Categories.FindAsync(item.CategoryId);
                model.RequestsByCategoryData.Add(new CategoryDataPoint
                {
                    CategoryName = category?.CategoryName ?? "Unknown",
                    RequestCount = item.Count
                });
            }

            // Financial Summary
            var currentMonthTransactions = _context.RentalTransactions
                .Where(t => (t.RentalStartDate ?? new DateTime()).Month == today.Month && (t.RentalStartDate ?? new DateTime()).Year == today.Year);

            model.FinancialSummary.CurrentMonthRevenue = await currentMonthTransactions
                .SumAsync(t => t.AmountPaid ?? 0);

            model.FinancialSummary.PendingPayments = await _context.RentalTransactions
                .Where(t => t.PaymentStatusId == 2) // 2 is Unpaid status
                .SumAsync(t => t.TotalFee ?? 0);

            model.FinancialSummary.TotalRevenue = await _context.RentalTransactions
                .Where(t => t.PaymentStatusId == 1) // 1 is Paid status
                .SumAsync(t => t.AmountPaid ?? 0);

            // Equipment Status
            model.EquipmentStatusData.TotalEquipment = await _context.Equipment.CountAsync();
            model.EquipmentStatusData.AvailableEquipment = await _context.Equipment
                .Where(e => e.AvailabilityStatusId == 1) // 1 is Available status
                .CountAsync();
            model.EquipmentStatusData.OutOfStockEquipment = await _context.Equipment
                .Where(e => e.AvailabilityStatusId == 2) // 2 is Out of Stock status
                .CountAsync();
            model.EquipmentStatusData.MaintenanceEquipment = await _context.Equipment
                .Where(e => e.AvailabilityStatusId == 3) // 3 is Under Maintenance status
                .CountAsync();

            // 7. Top Rented Equipment
            var topEquipment = await _context.RentalTransactions
                .GroupBy(t => t.EquipmentId)
                .Select(g => new { EquipmentId = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .Take(5)
                .ToListAsync();

            foreach (var item in topEquipment)
            {
                var equipment = await _context.Equipment.FindAsync(item.EquipmentId);
                model.TopRentedEquipment.Add(new EquipmentRentalData
                {
                    EquipmentName = equipment?.EquipmentName ?? "Unknown",
                    RentalCount = item.Count
                });
            }

            // 8. Recent Transactions
            var recentTransactions = await _context.RentalTransactions
                .OrderByDescending(t => t.RentalStartDate)
                .Take(10)
                .ToListAsync();

            foreach (var transaction in recentTransactions)
            {
                var equipment = await _context.Equipment.FindAsync(transaction.EquipmentId);
                var request = await _context.RentalRequests.FindAsync(transaction.RequestId);
                var user = request != null ? await _context.Users.FindAsync(request.UserId) : null;

                model.RecentTransactions.Add(new RecentTransactionData
                {
                    TransactionId = transaction.TransactionId,
                    EquipmentName = equipment?.EquipmentName ?? "Unknown",
                    UserName = user?.UserName ?? "Unknown",
                    RentalDate = transaction.RentalStartDate ?? new DateTime(),
                    ReturnDueDate = transaction.RentalReturnDate ?? new DateTime(),
                    Amount = transaction.AmountPaid ?? 0
                });
            }
        }

        // Category-specific dashboard for managers
        public async Task<IActionResult> CategoryDashboard(int categoryId)
        {
            // Check if user is authorized for this category
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);

            // For simplicity, we're assuming CategoryManager role has access to all categories
            // In a real app, you might have a user-category mapping table to check permissions
            if (!User.IsInRole("Administrator") && !User.IsInRole("CategoryManager"))
            {
                return Forbid();
            }

            var category = await _context.Categories.FindAsync(categoryId);
            if (category == null)
            {
                return NotFound();
            }

            var categoryDashboardViewModel = new CategoryDashboardViewModel
            {
                CategoryId = categoryId,
                CategoryName = category.CategoryName,
                // We'll populate these in the next steps
                TotalEquipment = 0,
                AvailableEquipment = 0,
                PendingRequests = 0,
                TotalRevenue = 0
            };

            await PopulateCategoryDashboardMetrics(categoryDashboardViewModel);

            return View(categoryDashboardViewModel);
        }

        private async Task PopulateCategoryDashboardMetrics(CategoryDashboardViewModel model)
        {
            // Get equipment in this category
            var categoryEquipment = _context.Equipment.Where(e => e.CategoryId == model.CategoryId);

            // Total equipment count
            model.TotalEquipment = await categoryEquipment.CountAsync();

            // Available equipment count
            model.AvailableEquipment = await categoryEquipment
                .Where(e => e.AvailabilityStatusId == 1) // Assuming 1 is Available status
                .CountAsync();

            // Pending requests count
            model.PendingRequests = await _context.RentalRequests
                .Join(_context.Equipment, r => r.EquipmentId, e => e.EquipmentId, (r, e) => new { Request = r, Equipment = e })
                .Where(x => x.Equipment.CategoryId == model.CategoryId && x.Request.RequestStatusId == 1) // Assuming 1 is Pending status
                .CountAsync();

            // Total revenue from this category
            model.TotalRevenue = await _context.RentalTransactions
                .Join(_context.Equipment, t => t.EquipmentId, e => e.EquipmentId, (t, e) => new { Transaction = t, Equipment = e })
                .Where(x => x.Equipment.CategoryId == model.CategoryId)
                .SumAsync(x => x.Transaction.AmountPaid ?? 0);

            // Equipment utilization (percentage of equipment currently rented)
            if (model.TotalEquipment > 0)
            {
                var rentedCount = await categoryEquipment
                    .Where(e => e.AvailabilityStatusId == 2) // Assuming 2 is Rented status
                    .CountAsync();
                model.UtilizationRate = (decimal)rentedCount / model.TotalEquipment * 100;
            }
            else
            {
                model.UtilizationRate = 0;
            }

            // Recent transactions in this category
            var recentTransactions = await _context.RentalTransactions
                .Join(_context.Equipment, t => t.EquipmentId, e => e.EquipmentId, (t, e) => new { Transaction = t, Equipment = e })
                .Where(x => x.Equipment.CategoryId == model.CategoryId)
                .OrderByDescending(x => x.Transaction.RentalStartDate)
                .Take(5)
                .Select(x => new CategoryTransactionData
                {
                    TransactionId = x.Transaction.TransactionId,
                    EquipmentName = x.Equipment.EquipmentName ?? "Unknown",
                    RentalDate = x.Transaction.RentalStartDate ?? new DateTime(),
                    Amount = x.Transaction.AmountPaid ?? 0
                })
                .ToListAsync();

            model.RecentTransactions = recentTransactions;
        }

        public async Task<IActionResult> DamagedEquipmentReport()
        {
            var damagedEquipment = await _context.Equipment
                .Where(e => e.ConditionId == 3) // Assuming 3 is Damaged condition
                .Include(e => e.Category)
                .OrderBy(e => e.Category.CategoryName)
                .ThenBy(e => e.EquipmentName)
                .ToListAsync();

            return View(damagedEquipment);
        }

        public async Task<IActionResult> ExportDamagedEquipmentReport()
        {
            var damagedEquipment = await _context.Equipment
                .Where(e => e.ConditionId == 3) // Assuming 3 is Damaged condition
                .Include(e => e.Category)
                .OrderBy(e => e.Category.CategoryName)
                .ToListAsync();

            // Create CSV data
            var csv = new StringBuilder();
            csv.AppendLine("ID,Name,Category,Rental Price,Cost Value");

            foreach (var item in damagedEquipment)
            {
                csv.AppendLine($"{item.EquipmentId},{item.EquipmentName},{item.Category?.CategoryName ?? "Unknown"}," +
                              $"{item.RentalPrice:C2},{item.Cost:C2}");
            }

            byte[] bytes = Encoding.UTF8.GetBytes(csv.ToString());
            return File(bytes, "text/csv", "damaged_equipment_report.csv");
        }

        public async Task<IActionResult> OverdueRentalsReport()
        {
            var today = DateTime.Today;
            var overdueRentals = await _context.RentalTransactions
                .Where(t => t.RentalReturnDate < today && !_context.ReturnRecords.Any(r => r.TransactionId == t.TransactionId))
                .Include(t => t.Equipment)
                .Include(t => t.Request)
                    .ThenInclude(r => r.User)
                .OrderBy(t => t.RentalReturnDate)
                .ToListAsync();

            return View(overdueRentals);
        }

        public async Task<IActionResult> ExportOverdueRentalsReport()
        {
            var today = DateTime.Today;
            var overdueRentals = await _context.RentalTransactions
                .Where(t => t.RentalReturnDate < today && !_context.ReturnRecords.Any(r => r.TransactionId == t.TransactionId))
                .Include(t => t.Equipment)
                .Include(t => t.Request)
                    .ThenInclude(r => r.User)
                .OrderBy(t => t.RentalReturnDate)
                .ToListAsync();

            // Create CSV data
            var csv = new StringBuilder();
            csv.AppendLine("Transaction ID,Equipment,User,Return Due Date,Days Overdue,Total Fee,Contact Info");

            foreach (var item in overdueRentals)
            {
                var daysOverdue = (today - item.RentalReturnDate)?.Days ?? 0;
                csv.AppendLine($"{item.TransactionId},{item.Equipment?.EquipmentName ?? "Unknown"}," +
                              $"{item.Request?.User?.UserName ?? "Unknown"},{(item.RentalReturnDate ?? new DateTime()).ToShortDateString()}," +
                              $"{daysOverdue},{item.RentalFee:C2},{item.Request?.User?.Email ?? "Unknown"}");
            }

            byte[] bytes = Encoding.UTF8.GetBytes(csv.ToString());
            return File(bytes, "text/csv", "overdue_rentals_report.csv");
        }

        public async Task<IActionResult> FinancialReport(DateTime? startDate, DateTime? endDate)
        {
            startDate ??= DateTime.Today.AddMonths(-1);
            endDate ??= DateTime.Today;

            var transactions = await _context.RentalTransactions
                .Where(t => t.RentalStartDate >= startDate && t.RentalStartDate <= endDate)
                .Include(t => t.Equipment)
                    .ThenInclude(e => e.Category)
                .Include(t => t.PaymentStatus)
                .Include(t => t.Request)
                    .ThenInclude(r => r.User)
                .OrderByDescending(t => t.RentalStartDate)
                .ToListAsync();

            ViewBag.StartDate = startDate.Value;
            ViewBag.EndDate = endDate.Value;
            ViewBag.TotalRevenue = transactions.Sum(t => t.AmountPaid);
            ViewBag.PaidRevenue = transactions
                .Where(t => t.PaymentStatusId == 1) // Assuming 1 is Paid status
                .Sum(t => t.AmountPaid);
            ViewBag.PendingRevenue = transactions
                .Where(t => t.PaymentStatusId == 2) // Assuming 2 is Pending payment status
                .Sum(t => t.AmountPaid);

            // Group by category for summary
            ViewBag.CategorySummary = transactions
                .GroupBy(t => t.Equipment?.Category?.CategoryName)
                .Select(g => new {
                    CategoryName = g.Key,
                    TransactionCount = g.Count(),
                    Revenue = g.Sum(t => t.AmountPaid)
                })
                .OrderByDescending(x => x.Revenue)
                .ToList();

            return View(transactions);
        }

        public async Task<IActionResult> ExportFinancialReport(DateTime? startDate, DateTime? endDate)
        {
            startDate ??= DateTime.Today.AddMonths(-1);
            endDate ??= DateTime.Today;

            var transactions = await _context.RentalTransactions
                .Where(t => t.RentalStartDate >= startDate && t.RentalStartDate <= endDate)
                .Include(t => t.Equipment)
                    .ThenInclude(e => e.Category)
                .Include(t => t.PaymentStatus)
                .Include(t => t.Request)
                    .ThenInclude(r => r.User)
                .OrderByDescending(t => t.RentalStartDate)
                .ToListAsync();

            // Create CSV data
            var csv = new StringBuilder();
            csv.AppendLine("Transaction ID,Equipment,Category,User,Rental Date,Return Date,Amount,Status");

            foreach (var item in transactions)
            {
                csv.AppendLine($"{item.TransactionId},{item.Equipment.EquipmentName ?? "Unknown"}," +
                              $"{item.Equipment?.Category?.CategoryName ?? "Unknown"},{item.Request?.User?.UserName ?? "Unknown"}," +
                              $"{item.RentalStartDate?.ToShortDateString() ?? "--/--/----"},{item.RentalReturnDate?.ToShortDateString() ?? "--/--/----"}," +
                              $"{item.AmountPaid:C2},{item.PaymentStatus?.PaymentStatusName ?? "Unknown"}");
            }

            byte[] bytes = Encoding.UTF8.GetBytes(csv.ToString());
            return File(bytes, "text/csv", $"financial_report_{startDate:yyyyMMdd}_to_{endDate:yyyyMMdd}.csv");
        }

        public async Task<IActionResult> ExportEquipmentData()
        {
            var equipment = await _context.Equipment
                .Include(e => e.Category)
                .Include(e => e.AvailabilityStatus)
                .Include(e => e.Condition)
                .ToListAsync();

            // Create CSV data
            var csv = new StringBuilder();
            csv.AppendLine("ID,Name,Category,Status,Condition,Rental Price");

            foreach (var item in equipment)
            {
                csv.AppendLine($"{item.EquipmentId},{item.EquipmentName},{item.Category?.CategoryName ?? "Unknown"}," +
                              $"{item.AvailabilityStatus?.AvailabilityStatusName ?? "Unknown"},{item.Condition?.ConditionName ?? "Unknown"}," +
                              $"{item.RentalPrice:C2}");
            }

            byte[] bytes = Encoding.UTF8.GetBytes(csv.ToString());
            return File(bytes, "text/csv", "equipment_data.csv");
        }

        public async Task<IActionResult> ExportTransactionData(DateTime? startDate, DateTime? endDate)
        {
            startDate ??= DateTime.Today.AddMonths(-1);
            endDate ??= DateTime.Today;

            var transactions = await _context.RentalTransactions
                .Where(t => t.RentalStartDate >= startDate && t.RentalStartDate <= endDate)
                .Include(t => t.Equipment)
                .Include(t => t.Request).ThenInclude(r => r.User)
                .Include(t => t.PaymentStatus)
                .ToListAsync();

            // Create CSV data
            var csv = new StringBuilder();
            csv.AppendLine("ID,Equipment,User,Rental Date,Return Due Date,Amount,Payment Status");

            foreach (var item in transactions)
            {
                csv.AppendLine($"{item.TransactionId},{item.Equipment?.EquipmentName ?? "Unknown"},{item.Request?.User?.UserName ?? "Unknown"}," +
                              $"{item.RentalStartDate?.ToShortDateString() ?? "----"},{item.RentalReturnDate?.ToShortDateString() ?? "----"}," +
                              $"{item.AmountPaid:C2},{item.PaymentStatus?.PaymentStatusName ?? "Unknown"}");
            }

            byte[] bytes = Encoding.UTF8.GetBytes(csv.ToString());
            return File(bytes, "text/csv", "transaction_data.csv");
        }
    }
}
