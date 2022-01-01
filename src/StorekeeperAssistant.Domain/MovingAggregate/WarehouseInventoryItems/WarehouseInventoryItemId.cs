using BuildingBlocks.Domain;
using System;

namespace StorekeeperAssistant.Domain.MovingAggregate.WarehouseInventoryItems
{
    public class WarehouseInventoryItemId : EntityId
    {
        public WarehouseInventoryItemId(Guid value) : base(value)
        {
        }
    }
}
