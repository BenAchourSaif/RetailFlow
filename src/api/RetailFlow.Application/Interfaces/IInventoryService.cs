using RetailFlow.Application.DTOs.Inventory; 

namespace RetailFlow.Application.Interfaces
{
    public interface IInventoryService
    {
        Task<InventoryResponse> ReceiveStockAsync(
        ReceiveStockRequest request,
        CancellationToken cancellationToken = default);

        Task<InventoryResponse?> GetAsync(
            Guid storeId,
            Guid productId,
            CancellationToken cancellationToken = default);

        Task<InventoryResponse> RemoveStockAsync(
            Guid storeId,
            Guid productId,
            decimal quantity,
            CancellationToken cancellationToken = default);
    }
}
