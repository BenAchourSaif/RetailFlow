using Microsoft.AspNetCore.Mvc;
using RetailFlow.Application.DTOs.Inventory;
using RetailFlow.Application.Interfaces;

namespace RetailFlow.Api.Controllers
{

    [ApiController]
    [Route("api/inventory")]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;

        public InventoryController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        [HttpPost("receive")]
        public async Task<ActionResult<InventoryResponse>> ReceiveStock(
            [FromBody] ReceiveStockRequest request,
            CancellationToken cancellationToken)
        {
            var inventory = await _inventoryService.ReceiveStockAsync(
                request,
                cancellationToken);

            return Ok(inventory);
        }

        [HttpGet("{storeId:guid}/{productId:guid}")]
        public async Task<ActionResult<InventoryResponse>> Get(
            Guid storeId,
            Guid productId,
            CancellationToken cancellationToken)
        {
            var inventory = await _inventoryService.GetAsync(
                storeId,
                productId,
                cancellationToken);

            if (inventory is null)
                return NotFound();

            return Ok(inventory);
        }
    }
}
