using RetailFlow.Application.DTOs.Inventory;
using System;
using System.Collections.Generic;
using System.Text;

namespace RetailFlow.Application.Interfaces
{
    public interface IInventoryService
    {
        InventoryResponse ReceiveStock(ReceiveStockRequest request);

        InventoryResponse? Get(Guid storeId, Guid productId);

        InventoryResponse RemoveStock(
            Guid storeId,
            Guid productId,
            decimal quantity);

    }
}
