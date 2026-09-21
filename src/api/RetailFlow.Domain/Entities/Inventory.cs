using System;
using System.Collections.Generic;
using System.Text;

namespace RetailFlow.Domain.Entities
{
    public class Inventory
    {
        public Guid Id { get; set; }

        public Guid StoreId { get; set; }

        public Store Store { get; set; } = null!;

        public Guid ProductId { get; set; }

        public Product Product { get; set; } = null!;

        public decimal Quantity { get; set; }

        public decimal AverageCost { get; set; }

        public ICollection<StockMovement> Movements { get; set; } = new List<StockMovement>();




        public void AddStock(decimal quantity, decimal unitCost)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero.");

            if (unitCost < 0)
                throw new ArgumentException("Unit cost cannot be negative.");

            var totalCurrentValue = Quantity * AverageCost;
            var totalIncomingValue = quantity * unitCost;

            Quantity += quantity;

            AverageCost = Quantity == 0
                ? 0
                : (totalCurrentValue + totalIncomingValue) / Quantity;
        }


        public void RemoveStock(decimal quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero.");

            if (quantity > Quantity)
                throw new InvalidOperationException("Insufficient stock.");

            Quantity -= quantity;

            if (Quantity == 0)
                AverageCost = 0;
        }

    }
}
