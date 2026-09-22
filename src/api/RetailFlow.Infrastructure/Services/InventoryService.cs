using RetailFlow.Application.DTOs.Inventory;
using RetailFlow.Application.Interfaces;
using RetailFlow.Domain.Entities;
using RetailFlow.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace RetailFlow.Infrastructure.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly RetailFlowDbContext _db;
        private readonly IInventoryRepository _inventoryRepository;
        public InventoryService(
                  RetailFlowDbContext db,
                  IInventoryRepository inventoryRepository)
        {
            _db = db;
            _inventoryRepository = inventoryRepository;
        }

        public async Task<InventoryResponse> ReceiveStockAsync(
            ReceiveStockRequest request,
            CancellationToken cancellationToken = default)
        {

            var storeExists = _db.Stores
    .Any(x => x.Id == request.StoreId);

            if (!storeExists)
                throw new KeyNotFoundException("Store not found.");

            var productExists = _db.Products
                .Any(x => x.Id == request.ProductId);

            if (!productExists)
                throw new KeyNotFoundException("Product not found.");


            var inventory =
                await _inventoryRepository.GetByStoreAndProductAsync(
                    request.StoreId,
                    request.ProductId,
                    cancellationToken);

            if (inventory is null)
            {
                inventory = new Inventory
                {
                    Id = Guid.NewGuid(),
                    StoreId = request.StoreId,
                    ProductId = request.ProductId
                };

                await _inventoryRepository.AddAsync(
                    inventory,
                    cancellationToken);
            }

            inventory.AddStock(
                request.Quantity,
                request.UnitCost);

            await _inventoryRepository.SaveChangesAsync(
                cancellationToken);

            return MapToResponse(inventory);
        }

        public async Task<InventoryResponse?> GetAsync(
            Guid storeId,
            Guid productId,
            CancellationToken cancellationToken = default)
        {
            var inventory =
                await _inventoryRepository.GetByStoreAndProductAsync(
                    storeId,
                    productId,
                    cancellationToken);

            return inventory is null
                ? null
                : MapToResponse(inventory);
        }

        public async Task<InventoryResponse> RemoveStockAsync(
            Guid storeId,
            Guid productId,
            decimal quantity,
            CancellationToken cancellationToken = default)
        {

            var storeExists = await _db.Stores
         .AnyAsync(x => x.Id == storeId, cancellationToken);

            if (!storeExists)
                throw new KeyNotFoundException("Store not found.");

            var productExists = await _db.Products
                .AnyAsync(x => x.Id == productId, cancellationToken);

            if (!productExists)
                throw new KeyNotFoundException("Product not found.");


            var inventory =
                await _inventoryRepository.GetByStoreAndProductAsync(
                    storeId,
                    productId,
                    cancellationToken);

            if (inventory is null)
                throw new KeyNotFoundException("Inventory not found.");

            inventory.RemoveStock(quantity);

            await _inventoryRepository.SaveChangesAsync(
                cancellationToken);

            return MapToResponse(inventory);
        }

        private static InventoryResponse MapToResponse(
            Inventory inventory)
        {
            return new InventoryResponse
            {
                Id = inventory.Id,
                StoreId = inventory.StoreId,
                ProductId = inventory.ProductId,
                Quantity = inventory.Quantity,
                AverageCost = inventory.AverageCost
            };
        }

    }
}
