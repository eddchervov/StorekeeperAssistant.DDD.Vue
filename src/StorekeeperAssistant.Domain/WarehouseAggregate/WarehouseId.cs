using BuildingBlocks.Domain;
using System;

namespace StorekeeperAssistant.Domain.WarehouseAggregate
{
    public class WarehouseId : EntityId
    {
        public WarehouseId(Guid value) : base(value)
        {
        }
    }
}
