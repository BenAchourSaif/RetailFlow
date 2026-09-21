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
        public ActionResult<InventoryResponse> ReceiveStock(
            [FromBody] ReceiveStockRequest request)
        {
            var inventory = _inventoryService.ReceiveStock(request);

            return Ok(inventory);
        }

        [HttpGet("{storeId:guid}/{productId:guid}")]
        public ActionResult<InventoryResponse> Get(
            Guid storeId,
            Guid productId)
        {
            var inventory = _inventoryService.Get(storeId, productId);

            if (inventory is null)
                return NotFound();

            return Ok(inventory);
        }
    }
}
