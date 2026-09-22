using Microsoft.AspNetCore.Mvc;
using RetailFlow.Application.DTOs.Tenants;
using RetailFlow.Application.Interfaces;

namespace RetailFlow.Api.Controllers
{
    [ApiController]
    [Route("api/tenants")]
    public class TenantController : ControllerBase
    {
        private readonly ITenantService _tenantService;

        public TenantController(ITenantService tenantService)
        {
            _tenantService = tenantService;
        }

        [HttpPost]
        public ActionResult<TenantResponse> Create(
            [FromBody] CreateTenantRequest request)
        {
            var tenant = _tenantService.Create(request);

            return Created($"/api/tenants/{tenant.Id}", tenant);
        }
    }
}
