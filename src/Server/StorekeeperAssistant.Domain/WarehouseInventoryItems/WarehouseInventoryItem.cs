using BuildingBlocks.Domain;
using StorekeeperAssistant.Domain.InventoryItems;
using StorekeeperAssistant.Domain.Movings;
using StorekeeperAssistant.Domain.Warehouses;
using System;

namespace StorekeeperAssistant.Domain.WarehouseInventoryItems;

public sealed class WarehouseInventoryItem : Entity, IAggregateRoot
{
    public WarehouseInventoryItemId Id { get; init; }
    public MovingId MovingId { get; init; }
    public InventoryItemId InventoryItemId { get; init; }
    public WarehouseId WarehouseId { get; init; }
    public WarehouseInventoryItemCount Count { get; init; }
    public DateTime Date { get; init; }

    #region ctor
#nullable disable
    WarehouseInventoryItem() { }
#nullable enable
    #endregion

    public static WarehouseInventoryItem Create(
        WarehouseInventoryItemId id,
        MovingId movingId,
        InventoryItemId inventoryItemId,
        WarehouseId warehouseId,
        WarehouseInventoryItemCount count,
        DateTime Date)
    {
        return new WarehouseInventoryItem
        {
            Id = id,
            MovingId = movingId,
            InventoryItemId = inventoryItemId,
            WarehouseId = warehouseId,
            Count = count,
            Date = Date
        };
    }
}
