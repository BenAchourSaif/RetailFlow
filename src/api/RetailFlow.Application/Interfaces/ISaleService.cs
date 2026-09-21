using RetailFlow.Application.DTOs.Sales;

namespace RetailFlow.Application.Interfaces
{
    public interface ISaleService
    {
        SaleResponse Create(CreateSaleRequest request);
    }
}
