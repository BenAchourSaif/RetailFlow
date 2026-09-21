using Microsoft.AspNetCore.Mvc;
using RetailFlow.Application.DTOs.Sales;
using RetailFlow.Application.Interfaces;

namespace RetailFlow.Api.Controllers
{

    [ApiController]
    [Route("api/sales")]
    public class SaleController : ControllerBase
    {
        private readonly ISaleService _saleService;

        public SaleController(ISaleService saleService)
        {
            _saleService = saleService;
        }

        [HttpPost]
        public ActionResult<SaleResponse> Create(
            [FromBody] CreateSaleRequest request)
        {
            var sale = _saleService.Create(request);

            return Created($"/api/sales/{sale.Id}", sale);
        }
    }
}
