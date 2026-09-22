using RetailFlow.Application.DTOs.Sales;

namespace RetailFlow.Application.Interfaces
{
    public interface ISaleService
    {
        Task<SaleResponse> CreateAsync(
    CreateSaleRequest request,
    CancellationToken cancellationToken = default);
    }
}
