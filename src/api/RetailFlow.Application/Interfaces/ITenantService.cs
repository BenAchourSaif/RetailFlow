using RetailFlow.Application.DTOs.Tenants;

namespace RetailFlow.Application.Interfaces
{
    public interface ITenantService
    {
        TenantResponse Create(CreateTenantRequest request);
    }
}
