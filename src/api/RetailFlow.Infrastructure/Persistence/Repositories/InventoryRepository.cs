using Microsoft.EntityFrameworkCore;
using RetailFlow.Application.Interfaces;
using RetailFlow.Domain.Entities;


namespace RetailFlow.Infrastructure.Persistence.Repositories
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly RetailFlowDbContext _dbContext;

        public InventoryRepository(RetailFlowDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Inventory?> GetByStoreAndProductAsync(
            Guid storeId,
            Guid productId,
            CancellationToken cancellationToken = default)
        {
            return await _dbContext.Inventories
                .FirstOrDefaultAsync(
                    inventory =>
                        inventory.StoreId == storeId &&
                        inventory.ProductId == productId,
                    cancellationToken);
        }

        public async Task AddAsync(
            Inventory inventory,
            CancellationToken cancellationToken = default)
        {
            await _dbContext.Inventories.AddAsync(
                inventory,
                cancellationToken);
        }

        public async Task SaveChangesAsync(
            CancellationToken cancellationToken = default)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
