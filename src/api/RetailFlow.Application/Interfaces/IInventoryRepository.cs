using RetailFlow.Domain.Entities;


namespace RetailFlow.Application.Interfaces
{
    public interface IInventoryRepository
    {
        Task<Inventory?> GetByStoreAndProductAsync(
     Guid storeId,
     Guid productId,
     CancellationToken cancellationToken = default);

        Task AddAsync(
            Inventory inventory,
            CancellationToken cancellationToken = default);

        Task SaveChangesAsync(
            CancellationToken cancellationToken = default);
    }
}
