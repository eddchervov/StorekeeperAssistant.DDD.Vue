using StorekeeperAssistant.Domain.InventoryItems;
using StorekeeperAssistant.Domain.Warehouses;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StorekeeperAssistant.DataAccess;

public static class DbInitializer
{
    public static void Initialize(AppDbContext context)
    {
        PopulateInventoryItems(context);
        PopulateWarehouses(context);

        context.SaveChanges();
    }

    private static void PopulateInventoryItems(AppDbContext context)
    {
        if (context.InventoryItems.Any() == false)
        {
            var inventoryItems = new List<InventoryItem>()
            {
                InventoryItem.Create(
                    new InventoryItemId(Guid.NewGuid()),
                    new InventoryItemName("Номенклатура А")),
                InventoryItem.Create(
                    new InventoryItemId(Guid.NewGuid()),
                    new InventoryItemName("Номенклатура Б")),
                InventoryItem.Create(
                    new InventoryItemId(Guid.NewGuid()),
                    new InventoryItemName("Номенклатура В")),
                InventoryItem.Create(
                    new InventoryItemId(Guid.NewGuid()),
                    new InventoryItemName("Номенклатура Г")),
                InventoryItem.Create(
                    new InventoryItemId(Guid.NewGuid()),
                    new InventoryItemName("Номенклатура Д")),
                InventoryItem.Create(
                    new InventoryItemId(Guid.NewGuid()),
                    new InventoryItemName("Номенклатура Е")),
                InventoryItem.Create(
                    new InventoryItemId(Guid.NewGuid()),
                    new InventoryItemName("Номенклатура Ж")),
            };

            context.InventoryItems.AddRange(inventoryItems);
        }
    }

    private static void PopulateWarehouses(AppDbContext context)
    {
        if (context.Warehouses.Any() == false)
        {
            var warehouses = new List<Warehouse>()
            {
                Warehouse.Create(
                    new WarehouseId(Guid.NewGuid()),
                    new WarehouseName("Склад А")),
                Warehouse.Create(
                    new WarehouseId(Guid.NewGuid()),
                    new WarehouseName("Склад Б")),
                Warehouse.Create(
                    new WarehouseId(Guid.NewGuid()),
                    new WarehouseName("Склад В"))
            };

            context.Warehouses.AddRange(warehouses);
        }
    }
}
