

namespace RetailFlow.Domain.Entities
{
    public class Product
    {

        public Guid Id { get; set; }

        public Guid TenantId { get; set; }

        public Tenant Tenant { get; set; } = null!;


        public string Sku { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;
        public decimal SellingPrice { get; private set; }

        public decimal VatRate { get; set; }

        public bool IsActive { get; set; } = true;



        public void UpdatePricing(decimal sellingPrice, decimal vatRate)
        {
            if (sellingPrice < 0)
                throw new ArgumentException("Selling price cannot be negative.");

            if (vatRate < 0 || vatRate > 100)
                throw new ArgumentException("VAT rate must be between 0 and 100.");

            SellingPrice = sellingPrice;
            VatRate = vatRate;
        }



        public void Deactivate()
        {
            IsActive = false;
        }

    }
}
