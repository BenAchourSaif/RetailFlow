

namespace RetailFlow.Application.DTOs.Stores
{
    public class StoreResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid TenantId { get; set; }
    }
}
