using System;
using System.Collections.Generic;
using System.Text;

namespace RetailFlow.Domain.Entities
{
    public class Tenant
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public ICollection<Store> Stores { get; set; } = new List<Store>();

        public Tenant()
	    {
	    }
    }
}
