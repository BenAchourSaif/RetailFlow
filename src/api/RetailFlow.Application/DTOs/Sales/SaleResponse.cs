using System;
using System.Collections.Generic;
using System.Text;

namespace RetailFlow.Application.DTOs.Sales
{
    public class SaleResponse
    {
        public Guid Id { get; set; }
        public Guid StoreId { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalCost { get; set; }
        public decimal Margin { get; set; }
    }
}
