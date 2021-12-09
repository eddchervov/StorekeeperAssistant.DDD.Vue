using StorekeeperAssistant.Domain.InventoryItemAggregate;
using StorekeeperAssistant.Domain.MovingAggregate;
using StorekeeperAssistant.Domain.WarehouseAggregate;
using System;

namespace StorekeeperAssistant.Domain.WarehouseInventoryItemAggregate
{
    public class WarehouseInventoryItem
    {
#nullable disable
        WarehouseInventoryItem() { }
#nullable enable

        public WarehouseInventoryItem(WarehouseInventoryItemId id,
            int count,
            WarehouseId warehouseId,
            InventoryItemId inventoryItemId,
            MovingId movingId)
        {
            Id = id;
            Count = count;
            Date = DateTime.UtcNow;
            WarehouseId = warehouseId;
            InventoryItemId = inventoryItemId;
            MovingId = movingId;
        }

        public WarehouseInventoryItemId Id { get; private set; }
        public int Count { get; private set; }
        public DateTime Date { get; private set; }
        public WarehouseId WarehouseId { get; private set; }
        public InventoryItemId InventoryItemId { get; private set; }
        public MovingId MovingId { get; private set; }
    }
}
