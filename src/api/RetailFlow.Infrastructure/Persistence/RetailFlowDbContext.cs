using Microsoft.EntityFrameworkCore;
using RetailFlow.Domain.Entities;

namespace RetailFlow.Infrastructure.Persistence
{
    public class RetailFlowDbContext : DbContext
    {

        public DbSet<Tenant> Tenants => Set<Tenant>();
        public DbSet<Store> Stores => Set<Store>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Inventory> Inventories => Set<Inventory>();
        public DbSet<StockMovement> StockMovements => Set<StockMovement>();
        public DbSet<Sale> Sales => Set<Sale>();
        public DbSet<SaleLine> SaleLines => Set<SaleLine>();


        public RetailFlowDbContext(DbContextOptions<RetailFlowDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(RetailFlowDbContext).Assembly);
        }


    }
}
