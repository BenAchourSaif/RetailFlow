using Microsoft.AspNetCore.Mvc;
using RetailFlow.Application.DTOs.Dashboard;
using RetailFlow.Application.Interfaces;

namespace RetailFlow.Api.Controllers
{

    [ApiController]
    [Route("api/dashboard")]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(
            IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet("summary")]
        public async Task<ActionResult<DashboardSummaryResponse>> GetSummary(
            CancellationToken cancellationToken)
        {
            var summary = await _dashboardService.GetSummaryAsync(
                cancellationToken);

            return Ok(summary);
        }
    }
}
