using BuildingBlocks.Domain;
using System;

namespace StorekeeperAssistant.Domain.InventoryItems
{
    public sealed class InventoryItemId : EntityId
    {
        public InventoryItemId(Guid value) : base(value)
        {
        }
    }
}
