using System;
using System.Collections.Generic;
using System.Text;

namespace RetailFlow.Domain.Entities
{
    public class Store
    {

        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid TenantId { get; set; }
        public Tenant Tenant { get; set; } = null!;

    }
}
