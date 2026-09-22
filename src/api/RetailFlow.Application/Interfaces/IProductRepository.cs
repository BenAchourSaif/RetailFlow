using RetailFlow.Domain.Entities;

namespace RetailFlow.Application.Interfaces
{
    public interface IProductRepository
    {
        Task<Product?> GetByIdAsync(
       Guid id,
       CancellationToken cancellationToken = default);

        Task AddAsync(
            Product product,
            CancellationToken cancellationToken = default);

        Task SaveChangesAsync(
            CancellationToken cancellationToken = default);
    }
}
