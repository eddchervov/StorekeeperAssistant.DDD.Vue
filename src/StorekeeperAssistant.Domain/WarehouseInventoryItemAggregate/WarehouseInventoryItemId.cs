using BuildingBlocks.Domain;
using System;

namespace StorekeeperAssistant.Domain.WarehouseInventoryItemAggregate
{
    public class WarehouseInventoryItemId : EntityId
    {
        public WarehouseInventoryItemId(Guid value) : base(value)
        {
        }
    }
}
