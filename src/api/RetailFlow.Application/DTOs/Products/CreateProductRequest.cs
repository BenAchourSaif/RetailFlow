
using System.ComponentModel.DataAnnotations;

namespace RetailFlow.Application.DTOs.Products
{
    public class CreateProductRequest
    {
        [Required]
        public string Sku { get; set; } = string.Empty;

        [Required]
        public string Name { get; set; } = string.Empty;
        [Range(0, double.MaxValue)]
        public decimal SellingPrice { get; set; }
        
        [Range(0, 100)]
        public decimal VatRate { get; set; }

    }
}
