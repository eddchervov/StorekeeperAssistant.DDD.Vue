using Microsoft.EntityFrameworkCore;
using StorekeeperAssistant.DataAccess.EntityConfigurations;
using StorekeeperAssistant.Domain.InventoryItemAggregate;
using StorekeeperAssistant.Domain.MovingAggregate;
using StorekeeperAssistant.Domain.MovingAggregate.WarehouseInventoryItems;
using StorekeeperAssistant.Domain.WarehouseAggregate;
using System;
using System.Collections.Generic;

namespace StorekeeperAssistant.DataAccess
{
    public class AppDbContext : DbContext
    {

#nullable disable
        public DbSet<Moving> Movings { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<InventoryItem> InventoryItems { get; set; }
        public DbSet<WarehouseInventoryItem> WarehouseInventoryItems { get; set; }
#nullable restore

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        internal IEnumerable<object> OrderByDescending(Func<object, object> p)
        {
            throw new NotImplementedException();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MovingEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new WarehouseEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new InventoryItemEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new MovingDetailEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new WarehouseInventoryItemEntityTypeConfiguration());
        }
    }
}
