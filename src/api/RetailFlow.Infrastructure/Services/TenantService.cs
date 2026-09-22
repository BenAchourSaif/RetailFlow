using RetailFlow.Application.DTOs.Tenants;
using RetailFlow.Application.Interfaces;
using RetailFlow.Domain.Entities;
using RetailFlow.Infrastructure.Persistence;

namespace RetailFlow.Infrastructure.Services
{
    public class TenantService : ITenantService
    {
        private readonly RetailFlowDbContext _db;

        public TenantService(RetailFlowDbContext db)
        {
            _db = db;
        }

        public TenantResponse Create(CreateTenantRequest request)
        {
            var tenant = new Tenant
            {
                Id = Guid.NewGuid(),
                Name = request.Name
            };

            _db.Tenants.Add(tenant);
            _db.SaveChanges();

            return new TenantResponse
            {
                Id = tenant.Id,
                Name = tenant.Name
            };
        }
    }
}
