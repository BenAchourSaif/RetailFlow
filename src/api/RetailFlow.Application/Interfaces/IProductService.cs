using RetailFlow.Application.DTOs.Products;

namespace RetailFlow.Application.Interfaces
{
    public interface IProductService
    {
         Task<ProductResponse> CreateAsync(
        CreateProductRequest request,
        CancellationToken cancellationToken = default);

    Task<ProductResponse?> GetAsync(
        Guid id,
        CancellationToken cancellationToken = default);
    }
}
