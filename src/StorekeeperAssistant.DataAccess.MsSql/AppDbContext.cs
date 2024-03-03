using Microsoft.EntityFrameworkCore;
using StorekeeperAssistant.Domain.InventoryItems;
using StorekeeperAssistant.Domain.Movings;
using StorekeeperAssistant.Domain.Movings.WarehouseInventoryItems;
using StorekeeperAssistant.Domain.Warehouses;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace StorekeeperAssistant.DataAccess;

public sealed class AppDbContext : DbContext
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
        //warning!!! with this approach you have no control over the order in which configurations are applied!!!
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
