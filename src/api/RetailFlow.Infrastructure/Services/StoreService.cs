using RetailFlow.Application.DTOs.Stores;
using RetailFlow.Application.Interfaces;
using RetailFlow.Infrastructure.Persistence;
using RetailFlow.Domain.Entities;

namespace RetailFlow.Infrastructure.Services
{
    public class StoreService : IStoreService
    {
        private readonly RetailFlowDbContext _db;

        public StoreService(RetailFlowDbContext db)
        {
            _db = db;
        }

        public StoreResponse Create(CreateStoreRequest request)
        {
            var tenantExists = _db.Tenants
                .Any(x => x.Id == request.TenantId);

            if (!tenantExists)
                throw new KeyNotFoundException("Tenant not found.");

            var store = new Store
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                TenantId = request.TenantId
            };

            _db.Stores.Add(store);
            _db.SaveChanges();

            return new StoreResponse
            {
                Id = store.Id,
                Name = store.Name,
                TenantId = store.TenantId
            };
        }
    }
}
