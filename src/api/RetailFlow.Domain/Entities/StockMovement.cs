using RetailFlow.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace RetailFlow.Domain.Entities
{
    public class StockMovement
    {
        public Guid Id { get; set; }

        public Guid InventoryId { get; set; }

        public Inventory Inventory { get; set; } = null!;

        public StockMovementType Type { get; set; }

        public decimal Quantity { get; set; }

        public decimal UnitCost { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
