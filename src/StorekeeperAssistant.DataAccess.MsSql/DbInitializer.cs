using StorekeeperAssistant.Domain.InventoryItemAggregate;
using StorekeeperAssistant.Domain.WarehouseAggregate;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StorekeeperAssistant.DataAccess
{
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
                    new InventoryItem(
                        new InventoryItemId(Guid.NewGuid()),
                        new InventoryItemName("Номенклатура А")),
                    new InventoryItem(
                        new InventoryItemId(Guid.NewGuid()),
                        new InventoryItemName("Номенклатура Б")),
                    new InventoryItem(
                        new InventoryItemId(Guid.NewGuid()),
                        new InventoryItemName("Номенклатура В")),
                    new InventoryItem(
                        new InventoryItemId(Guid.NewGuid()),
                        new InventoryItemName("Номенклатура Г")),
                    new InventoryItem(
                        new InventoryItemId(Guid.NewGuid()),
                        new InventoryItemName("Номенклатура Д")),
                    new InventoryItem(
                        new InventoryItemId(Guid.NewGuid()),
                        new InventoryItemName("Номенклатура Е")),
                    new InventoryItem(
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
                    new Warehouse(
                        new WarehouseId(Guid.NewGuid()),
                        new WarehouseName("Склад А")),
                    new Warehouse(
                        new WarehouseId(Guid.NewGuid()),
                        new WarehouseName("Склад Б")),
                    new Warehouse(
                        new WarehouseId(Guid.NewGuid()),
                        new WarehouseName("Склад В"))
                };

                context.Warehouses.AddRange(warehouses);
            }
        }
    }
}
