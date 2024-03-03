using System;

namespace StorekeeperAssistant.Domain.Warehouses;

public sealed class DepartureWarehouseId : WarehouseId
{
    public DepartureWarehouseId(Guid value) : base(value)
    {
    }
}
