using RetailFlow.Application.DTOs.Dashboard;
using RetailFlow.Application.Interfaces;
using RetailFlow.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace RetailFlow.Infrastructure.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly RetailFlowDbContext _db;
        private readonly ITenantContext _tenantContext;

        public DashboardService(
            RetailFlowDbContext db,
            ITenantContext tenantContext)
        {
            _db = db;
            _tenantContext = tenantContext;
        }

        public async Task<DashboardSummaryResponse> GetSummaryAsync(
            CancellationToken cancellationToken = default)
        {
            var tenantId = _tenantContext.TenantId;

            var productCount = await _db.Products
                .CountAsync(
                    x => x.TenantId == tenantId,
                    cancellationToken);

            var activeProductCount = await _db.Products
                .CountAsync(
                    x => x.TenantId == tenantId && x.IsActive,
                    cancellationToken);

            var inventory = await _db.Inventories
                .Where(x => x.Store.TenantId == tenantId)
                .Select(x => new
                {
                    x.Quantity,
                    x.AverageCost
                })
                .ToListAsync(cancellationToken);

            var sales = await _db.Sales
                .Where(x => x.Store.TenantId == tenantId)
                .Select(x => new
                {
                    x.TotalAmount,
                    x.TotalCost
                })
                .ToListAsync(cancellationToken);

            var inventoryQuantity = inventory.Sum(x => x.Quantity);

            var inventoryValue = inventory.Sum(
                x => x.Quantity * x.AverageCost);

            var salesAmount = sales.Sum(x => x.TotalAmount);
            var salesCost = sales.Sum(x => x.TotalCost);

            return new DashboardSummaryResponse
            {
                ProductCount = productCount,
                ActiveProductCount = activeProductCount,
                InventoryQuantity = inventoryQuantity,
                InventoryValue = inventoryValue,
                SalesCount = sales.Count,
                SalesAmount = salesAmount,
                SalesCost = salesCost,
                Margin = salesAmount - salesCost
            };
        }
    }
}
