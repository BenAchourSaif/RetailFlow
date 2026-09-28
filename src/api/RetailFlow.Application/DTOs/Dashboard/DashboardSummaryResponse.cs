
namespace RetailFlow.Application.DTOs.Dashboard
{
    public class DashboardSummaryResponse
    {
        public int ProductCount { get; set; }
        public int ActiveProductCount { get; set; }

        public decimal InventoryQuantity { get; set; }
        public decimal InventoryValue { get; set; }

        public int SalesCount { get; set; }
        public decimal SalesAmount { get; set; }
        public decimal SalesCost { get; set; }
        public decimal Margin { get; set; }
    }
}
