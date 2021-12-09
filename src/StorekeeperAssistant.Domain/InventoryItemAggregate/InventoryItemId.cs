using BuildingBlocks.Domain;
using System;

namespace StorekeeperAssistant.Domain.InventoryItemAggregate
{
    public class InventoryItemId : EntityId
    {
        public InventoryItemId(Guid value) : base(value)
        {
        }
    }
}
