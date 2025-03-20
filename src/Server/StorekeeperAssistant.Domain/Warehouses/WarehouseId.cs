using BuildingBlocks.Domain;
using System;

namespace StorekeeperAssistant.Domain.Warehouses;

public class WarehouseId : EntityId
{
    public WarehouseId(Guid value) : base(value)
    {
    }
}
