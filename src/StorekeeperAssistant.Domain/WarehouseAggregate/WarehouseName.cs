using BuildingBlocks.Domain;

namespace StorekeeperAssistant.Domain.WarehouseAggregate
{
    public class WarehouseName : StringNotEmpty
    {
        public WarehouseName(string value) : base(value)
        {
        }
    }
}
