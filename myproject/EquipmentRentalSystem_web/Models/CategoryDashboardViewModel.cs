namespace EquipmentRentalSystem_web.Models
{
    public class CategoryDashboardViewModel
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int TotalEquipment { get; set; }
        public int AvailableEquipment { get; set; }
        public int PendingRequests { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal UtilizationRate { get; set; }
        public List<CategoryTransactionData> RecentTransactions { get; set; }
    }

    public class CategoryTransactionData
    {
        public int TransactionId { get; set; }
        public string EquipmentName { get; set; }
        public DateTime RentalDate { get; set; }
        public decimal Amount { get; set; }
    }
}
