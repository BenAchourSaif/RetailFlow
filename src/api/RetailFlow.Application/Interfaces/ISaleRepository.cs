
using RetailFlow.Domain.Entities;

namespace RetailFlow.Application.Interfaces
{
    public interface ISaleRepository
    {
        Task<Sale?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default);

        Task AddAsync(
            Sale sale,
            CancellationToken cancellationToken = default);

        Task SaveChangesAsync(
            CancellationToken cancellationToken = default);
    }
}
