using BuildingBlocks.Domain;

namespace StorekeeperAssistant.Domain.InventoryItemAggregate
{
    public class InventoryItemName : StringNotEmpty
    {
        public InventoryItemName(string value) : base(value)
        {
        }
    }
}
