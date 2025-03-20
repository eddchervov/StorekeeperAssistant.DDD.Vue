using System;

namespace StorekeeperAssistant.Domain.Warehouses;

public sealed class ArrivalWarehouseId : WarehouseId
{
    public ArrivalWarehouseId(Guid value) : base(value)
    {
    }
}
