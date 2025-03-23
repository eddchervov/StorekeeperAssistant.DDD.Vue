using StorekeeperAssistant.Domain.InventoryItems;
using StorekeeperAssistant.Domain.Movings;
using StorekeeperAssistant.Domain.WarehouseInventoryItems;
using StorekeeperAssistant.Domain.Warehouses;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StorekeeperAssistant.Domain.Services;

public sealed class WarehouseInventoryItemService
{
    public record InventoryItemDto(Guid Id, int Count);

    public WarehouseInventoryItem CreateExpenseWarehouseInventoryItem(
        MovingId movingId,
        Warehouse warehouse,
        IEnumerable<WarehouseInventoryItem> lastWarehouseInventoryItems,
        DateTime date,
        InventoryItemDto inventoryItemDto)
    {
        var inventoryItemId = new InventoryItemId(inventoryItemDto.Id);

        var lastDepartureWarehouseInventoryItem = lastWarehouseInventoryItems.FirstOrDefault(x => x.WarehouseId == warehouse.Id && x.InventoryItemId == inventoryItemId)
            ?? throw new ArgumentException($"Склад отправления с id={warehouse.Id.Value} и номенклатурой id={inventoryItemDto.Id} не найден");

        var warehouseInventoryItemCount = lastDepartureWarehouseInventoryItem.Count.Value - inventoryItemDto.Count;
        if (warehouseInventoryItemCount < 0)
            throw new ArgumentException($"Нельзя расходовать номенклатуру id={inventoryItemDto.Id} в кол-ве: {inventoryItemDto.Count}. " +
                $"Недостаточно остатков на складе, остаток: {lastDepartureWarehouseInventoryItem.Count.Value}");

        return WarehouseInventoryItem.Create(
                new WarehouseInventoryItemId(Guid.NewGuid()),
                movingId,
                inventoryItemId,
                warehouse.Id,
                new WarehouseInventoryItemCount(warehouseInventoryItemCount),
                date
            );
    }

    public WarehouseInventoryItem CreateIncomeWarehouseInventoryItem(
        MovingId movingId,
        Warehouse warehouse,
        IEnumerable<WarehouseInventoryItem> lastWarehouseInventoryItems,
        DateTime date,
        InventoryItemDto inventoryItemDto)
    {
        var inventoryItemId = new InventoryItemId(inventoryItemDto.Id);

        var warehouseInventoryItemCount = inventoryItemDto.Count;
        var lastArrivalWarehouseInventoryItem = lastWarehouseInventoryItems.FirstOrDefault(x => x.WarehouseId == warehouse.Id && x.InventoryItemId == inventoryItemId);
        if (lastArrivalWarehouseInventoryItem != null) warehouseInventoryItemCount += lastArrivalWarehouseInventoryItem.Count.Value;

        return WarehouseInventoryItem.Create(
            new WarehouseInventoryItemId(Guid.NewGuid()),
                movingId,
                inventoryItemId,
                warehouse.Id,
                new WarehouseInventoryItemCount(warehouseInventoryItemCount),
                date
            );
    }
}
