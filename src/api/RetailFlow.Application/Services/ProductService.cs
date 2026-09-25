using RetailFlow.Application.DTOs.Products;
using RetailFlow.Application.Interfaces;
using RetailFlow.Domain.Entities;

namespace RetailFlow.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ITenantContext _tenantContext;
        public ProductService(IProductRepository productRepository, ITenantContext tenantContext)
        {
            _productRepository = productRepository;
            _tenantContext = tenantContext;
        }

        public async Task<ProductResponse> CreateAsync(
            CreateProductRequest request,
            CancellationToken cancellationToken = default)
        {
            var product = new Product
            {
                Id = Guid.NewGuid(),
                TenantId = _tenantContext.TenantId,
                Sku = request.Sku,
                Name = request.Name
            };

            product.UpdatePricing(
                request.SellingPrice,
                request.VatRate);

            await _productRepository.AddAsync(
                product,
                cancellationToken);

            await _productRepository.SaveChangesAsync(
                cancellationToken);

            return MapToResponse(product);
        }

        public async Task<ProductResponse?> GetAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            var product = await _productRepository.GetByIdAsync(
                id,
                cancellationToken);

            return product is null
                ? null
                : MapToResponse(product);
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
