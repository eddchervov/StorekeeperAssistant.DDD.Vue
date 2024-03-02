using BuildingBlocks.Domain;
using StorekeeperAssistant.Domain.InventoryItemAggregate;
using StorekeeperAssistant.Domain.WarehouseAggregate;
using System;

namespace StorekeeperAssistant.Domain.MovingAggregate.WarehouseInventoryItems
{
    public class WarehouseInventoryItem : Entity
    {
#nullable disable
        WarehouseInventoryItem() { }
#nullable enable

        public WarehouseInventoryItem(
            WarehouseInventoryItemId id,
            MovingId movingId,
            InventoryItemId inventoryItemId,
            WarehouseId warehouseId,
            WarehouseInventoryItemCount count)
        {
            Id = id;
            Count = count;
            Date = DateTime.UtcNow;
            WarehouseId = warehouseId;
            InventoryItemId = inventoryItemId;
            MovingId = movingId;
        }

        public WarehouseInventoryItemId Id { get; }
        public WarehouseInventoryItemCount Count { get; }
        public DateTime Date { get; }
        public WarehouseId WarehouseId { get; }
        public InventoryItemId InventoryItemId { get; }
        public MovingId MovingId { get; }
    }
}
