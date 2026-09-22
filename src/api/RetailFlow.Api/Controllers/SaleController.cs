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
        public async Task<ActionResult<SaleResponse>> Create(
                        [FromBody] CreateSaleRequest request,
                        CancellationToken cancellationToken)
        {
            var sale = await _saleService.CreateAsync(
                                            request,
                                            cancellationToken);

            return Created($"/api/sales/{sale.Id}", sale);
        }
    }
}
