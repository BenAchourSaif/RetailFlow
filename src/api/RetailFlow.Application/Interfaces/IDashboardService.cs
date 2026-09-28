using RetailFlow.Application.DTOs.Dashboard;

namespace RetailFlow.Application.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardSummaryResponse> GetSummaryAsync(
       CancellationToken cancellationToken = default);
    }
}
