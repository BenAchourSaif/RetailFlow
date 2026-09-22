
namespace RetailFlow.Application.DTOs.Stores
{
    public class CreateStoreRequest
    {
        public string Name { get; set; } = string.Empty;
        public Guid TenantId { get; set; }
    }
}
