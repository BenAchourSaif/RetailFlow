using Microsoft.EntityFrameworkCore;
using RetailFlow.Application.Interfaces;
using RetailFlow.Domain.Entities;

namespace RetailFlow.Infrastructure.Persistence.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly RetailFlowDbContext _dbContext;
        private readonly ITenantContext _tenantContext;
        public ProductRepository(RetailFlowDbContext dbContext, ITenantContext tenantContext)
        {
            _dbContext = dbContext;
            _tenantContext = tenantContext;
        }

        public async Task<Product?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            return await _dbContext.Products
                 .AsNoTracking()
                .FirstOrDefaultAsync(
                    product => product.Id == id && product.TenantId == _tenantContext.TenantId);
        }

        public async Task AddAsync(
            Product product,
            CancellationToken cancellationToken = default)
        {
            await _dbContext.Products.AddAsync(
                product,
                cancellationToken);
        }

        public async Task SaveChangesAsync(
            CancellationToken cancellationToken = default)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
