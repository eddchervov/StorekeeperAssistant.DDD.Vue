using BuildingBlocks.Domain;

namespace StorekeeperAssistant.Domain.Warehouses;

public sealed class WarehouseName : StringNotEmpty
{
    public WarehouseName(string value) : base(value)
    {
    }
}
