using System.ComponentModel.DataAnnotations;

namespace RetailFlow.Application.DTOs.Inventory
{
    public class ReceiveStockRequest
    {
        public Guid StoreId { get; set; }
        public Guid ProductId { get; set; }

        [Range(0.01, double.MaxValue)]
        public decimal Quantity { get; set; }

        [Range(0, double.MaxValue)]
        public decimal UnitCost { get; set; }
    }
}
