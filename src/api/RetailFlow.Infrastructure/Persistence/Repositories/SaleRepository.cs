using Microsoft.EntityFrameworkCore;
using RetailFlow.Application.Interfaces;
using RetailFlow.Domain.Entities;

namespace RetailFlow.Infrastructure.Persistence.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly RetailFlowDbContext _dbContext;

        public SaleRepository(RetailFlowDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Sale?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            return await _dbContext.Sales
                .Include(sale => sale.Lines)
                .FirstOrDefaultAsync(
                    sale => sale.Id == id,
                    cancellationToken);
        }

        public async Task AddAsync(
            Sale sale,
            CancellationToken cancellationToken = default)
        {
            await _dbContext.Sales.AddAsync(
                sale,
                cancellationToken);
        }

        public async Task SaveChangesAsync(
            CancellationToken cancellationToken = default)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
