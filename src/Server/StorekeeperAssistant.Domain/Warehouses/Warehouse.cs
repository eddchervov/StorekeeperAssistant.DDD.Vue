using BuildingBlocks.Domain;

namespace StorekeeperAssistant.Domain.Warehouses;

public sealed class Warehouse : Entity, IAggregateRoot
{
    public WarehouseId Id { get; init; }
    public WarehouseName Name { get; init; }

    public bool IsDeleted { get; }

    #region ctor
#nullable disable
    Warehouse() { }
#nullable enable

    private Warehouse(WarehouseId id, WarehouseName name)
    {
        Id = id;
        Name = name;
    }
    #endregion

    public static Warehouse Create(WarehouseId id, WarehouseName name)
    {
        return new Warehouse
        {
            Id = id,
            Name = name
        };
    }
}
