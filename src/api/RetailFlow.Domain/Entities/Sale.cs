using System;
using System.Collections.Generic;
using System.Text;

namespace RetailFlow.Domain.Entities
{
    public class Sale
    {
        public Guid Id { get; set; }

        public Guid StoreId { get; set; }

        public Store Store { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal TotalCost { get; set; }

        public ICollection<SaleLine> Lines { get; set; } = new List<SaleLine>();
    }
}
