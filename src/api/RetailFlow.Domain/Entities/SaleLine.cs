using System;
using System.Collections.Generic;
using System.Text;

namespace RetailFlow.Domain.Entities
{
    public class SaleLine
    {
        public Guid Id { get; set; }

        public Guid SaleId { get; set; }

        public Sale Sale { get; set; } = null!;

        public Guid ProductId { get; set; }

        public Product Product { get; set; } = null!;

        public decimal Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal UnitCost { get; set; }

        public decimal VatRate { get; set; }
    }
}
