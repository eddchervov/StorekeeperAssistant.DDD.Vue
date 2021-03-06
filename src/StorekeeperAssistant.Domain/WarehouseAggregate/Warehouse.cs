using BuildingBlocks.Domain;

namespace StorekeeperAssistant.Domain.WarehouseAggregate
{
    public class Warehouse : Entity, IAggregateRoot
    {
#nullable disable
        Warehouse() { }
#nullable enable

        public Warehouse(WarehouseId id, WarehouseName name)
        {
            Id = id;
            Name = name;
        }

        public WarehouseId Id { get; }
        public WarehouseName Name { get; }

        public bool IsDeleted { get; }
    }
}
