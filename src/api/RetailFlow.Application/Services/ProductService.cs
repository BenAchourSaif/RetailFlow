using RetailFlow.Application.DTOs.Products;
using RetailFlow.Application.Interfaces;
using RetailFlow.Domain.Entities;

namespace RetailFlow.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly Dictionary<Guid, Product> _products = new();

        public ProductResponse Create(CreateProductRequest request)
        {
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Sku = request.Sku,
                Name = request.Name
            };

            product.UpdatePricing(
                request.SellingPrice,
                request.VatRate
            );

            _products[product.Id] = product;

            return MapToResponse(product);
        }

        public ProductResponse? Get(Guid id)
        {
            return _products.TryGetValue(id, out var product)
                ? MapToResponse(product)
                : null;
        }

        private static ProductResponse MapToResponse(Product product)
        {
            return new ProductResponse
            {
                Id = product.Id,
                Sku = product.Sku,
                Name = product.Name,
                SellingPrice = product.SellingPrice,
                VatRate = product.VatRate,
                IsActive = product.IsActive
            };
        }
    }
}
