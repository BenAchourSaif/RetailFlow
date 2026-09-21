
using System.ComponentModel.DataAnnotations;

namespace RetailFlow.Application.DTOs.Sales
{
    public class CreateSaleRequest
    {
        public Guid StoreId { get; set; }
        [Required]
        [MinLength(1)]
        public List<CreateSaleLineRequest> Lines { get; set; } = new();
    }

    public class CreateSaleLineRequest
    {
        public Guid ProductId { get; set; }
        [Range(0.01, double.MaxValue)]
        public decimal Quantity { get; set; }
    }

}
