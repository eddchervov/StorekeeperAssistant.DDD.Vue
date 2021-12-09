namespace StorekeeperAssistant.Domain.WarehouseAggregate
{
    public class Warehouse
    {
#nullable disable
        Warehouse() { }
#nullable enable

        public Warehouse(WarehouseId id, WarehouseName name)
        {
            Id = id;
            Name = name;
        }

        public WarehouseId Id { get; private set; }
        public WarehouseName Name { get; private set; }

        public bool IsDeleted { get; private set; }
    }
}
