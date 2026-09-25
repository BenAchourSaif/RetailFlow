using RetailFlow.Application.Interfaces;

namespace RetailFlow.Infrastructure.MultiTenancy
{
    public class TenantContext : ITenantContext
    {
        public Guid TenantId { get; private set; }

        public void SetTenant(Guid tenantId)
        {
            TenantId = tenantId;
        }
    }
}
