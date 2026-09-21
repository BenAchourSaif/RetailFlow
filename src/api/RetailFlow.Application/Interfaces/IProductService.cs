using RetailFlow.Application.DTOs.Products;

namespace RetailFlow.Application.Interfaces
{
    public interface IProductService
    {
        ProductResponse Create(CreateProductRequest request);

        ProductResponse? Get(Guid id);
    }
}
