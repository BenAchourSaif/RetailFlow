

namespace RetailFlow.Application.DTOs.Products
{
    public class ProductResponse
    {
        public Guid Id { get; set; }

        public string Sku { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public decimal SellingPrice { get; set; }

        public decimal VatRate { get; set; }

        public bool IsActive { get; set; }
    }
}
