

namespace RetailFlow.Application.Interfaces
{
    public interface ITenantContext
    {
        Guid TenantId { get; }

        void SetTenant(Guid tenantId);
    }
}
