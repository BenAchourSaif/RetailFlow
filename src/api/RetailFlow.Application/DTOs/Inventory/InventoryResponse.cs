using System;
using System.Collections.Generic;
using System.Text;

namespace RetailFlow.Application.DTOs.Inventory
{
    public class InventoryResponse
    {
        public Guid Id { get; set; }
        public Guid StoreId { get; set; }
        public Guid ProductId { get; set; }
        public decimal Quantity { get; set; }
        public decimal AverageCost { get; set; }

    }
}
