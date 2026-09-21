using RetailFlow.Application.DTOs.Inventory;
using RetailFlow.Application.Interfaces;
using RetailFlow.Domain.Entities;

namespace RetailFlow.Application.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly Dictionary<string, Inventory> _inventories = new();

        public InventoryResponse ReceiveStock(ReceiveStockRequest request)
        {
            var key = $"{request.StoreId}:{request.ProductId}";

            if (!_inventories.TryGetValue(key, out var inventory))
            {
                inventory = new Inventory
                {
                    Id = Guid.NewGuid(),
                    StoreId = request.StoreId,
                    ProductId = request.ProductId
                };

                _inventories[key] = inventory;
            }

            inventory.AddStock(
                request.Quantity,
                request.UnitCost
            );

            return MapToResponse(inventory);
        }

        public InventoryResponse? Get(Guid storeId, Guid productId)
        {
            var key = $"{storeId}:{productId}";

            return _inventories.TryGetValue(key, out var inventory)
                ? MapToResponse(inventory)
                : null;
        }

        private static InventoryResponse MapToResponse(Inventory inventory)
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

        public InventoryResponse RemoveStock(
                                            Guid storeId,
                                            Guid productId,
                                            decimal quantity)
        {
            var key = $"{storeId}:{productId}";

            if (!_inventories.TryGetValue(key, out var inventory))
                throw new InvalidOperationException("Inventory not found.");

            inventory.RemoveStock(quantity);

            return MapToResponse(inventory);
        }

    }
}
