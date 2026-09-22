using Microsoft.AspNetCore.Mvc;
using RetailFlow.Application.DTOs.Stores;
using RetailFlow.Application.Interfaces;

namespace RetailFlow.Api.Controllers
{
    [ApiController]
    [Route("api/stores")]
    public class StoreController : ControllerBase
    {
        private readonly IStoreService _storeService;

        public StoreController(IStoreService storeService)
        {
            _storeService = storeService;
        }

        [HttpPost]
        public ActionResult<StoreResponse> Create(
            [FromBody] CreateStoreRequest request)
        {
            var store = _storeService.Create(request);

            return Created($"/api/stores/{store.Id}", store);
        }
    }
}
