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
                    new InventoryItemName("Яблоки")),
                InventoryItem.Create(
                    new InventoryItemId(Guid.NewGuid()),
                    new InventoryItemName("Бананы")),
                InventoryItem.Create(
                    new InventoryItemId(Guid.NewGuid()),
                    new InventoryItemName("Картошка")),
                InventoryItem.Create(
                    new InventoryItemId(Guid.NewGuid()),
                    new InventoryItemName("Лук")),
                InventoryItem.Create(
                    new InventoryItemId(Guid.NewGuid()),
                    new InventoryItemName("Морковь")),
                InventoryItem.Create(
                    new InventoryItemId(Guid.NewGuid()),
                    new InventoryItemName("Груши")),
                InventoryItem.Create(
                    new InventoryItemId(Guid.NewGuid()),
                    new InventoryItemName("Свекла")),
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
                    new WarehouseName("Склад г. Новосибирск")),
                Warehouse.Create(
                    new WarehouseId(Guid.NewGuid()),
                    new WarehouseName("Склад г. Москва")),
                Warehouse.Create(
                    new WarehouseId(Guid.NewGuid()),
                    new WarehouseName("Склад г. Санкт-Петербург")),
                Warehouse.Create(
                    new WarehouseId(Guid.NewGuid()),
                    new WarehouseName("Склад г. Кемерово")),
                Warehouse.Create(
                    new WarehouseId(Guid.NewGuid()),
                    new WarehouseName("Склад г. Владивосток")),
                Warehouse.Create(
                    new WarehouseId(Guid.NewGuid()),
                    new WarehouseName("Склад г. Красноярск")),
                Warehouse.Create(
                    new WarehouseId(Guid.NewGuid()),
                    new WarehouseName("Склад г. Омск")),
                Warehouse.Create(
                    new WarehouseId(Guid.NewGuid()),
                    new WarehouseName("Склад г. Сочи")),
                Warehouse.Create(
                    new WarehouseId(Guid.NewGuid()),
                    new WarehouseName("Склад г. Ярославль")),
                Warehouse.Create(
                    new WarehouseId(Guid.NewGuid()),
                    new WarehouseName("Склад г. Томск")),
                Warehouse.Create(
                    new WarehouseId(Guid.NewGuid()),
                    new WarehouseName("Склад г. Якутск"))
            };

            context.Warehouses.AddRange(warehouses);
        }
    }
}
