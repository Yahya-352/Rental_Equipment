namespace EquipmentRentalSystem_web.Models
{
    public class DashboardViewModel
    {
        public int PendingRequestsCount { get; set; }
        public int ApprovedRequestsCount { get; set; }
        public int RejectedRequestsCount { get; set; }
        public int CancelledRequestsCount { get; set; }
        public int CompletedRequestsCount { get; set; }
        public int OverdueRequestsCount { get; set; }
        public int DamagedEquipmentCount { get; set; }
        public int LostEquipmentCount { get; set; }
        public List<CategoryDataPoint> RequestsByCategoryData { get; set; }
        public FinancialSummaryData FinancialSummary { get; set; }
        public EquipmentStatusData EquipmentStatusData { get; set; }
        public List<EquipmentRentalData> TopRentedEquipment { get; set; }
        public List<RecentTransactionData> RecentTransactions { get; set; }
    }

    public class EquipmentStatusData
    {
        public int TotalEquipment { get; set; }
        public int AvailableEquipment { get; set; }
        public int OutOfStockEquipment { get; set; }
        public int MaintenanceEquipment { get; set; }
    }

    public class CategoryDataPoint
    {
        public string CategoryName { get; set; }
        public int RequestCount { get; set; }
    }

    public class FinancialSummaryData
    {
        public decimal CurrentMonthRevenue { get; set; }
        public decimal PendingPayments { get; set; }
        public decimal TotalRevenue { get; set; }
    }

    public class EquipmentRentalData
    {
        public string EquipmentName { get; set; }
        public int RentalCount { get; set; }
    }

    public class RecentTransactionData
    {
        public int TransactionId { get; set; }
        public string EquipmentName { get; set; }
        public string UserName { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime ReturnDueDate { get; set; }
        public decimal Amount { get; set; }
    }

}
