using BuildingBlocks.Domain;

namespace StorekeeperAssistant.Domain.InventoryItems
{
    public sealed class InventoryItemName : StringNotEmpty
    {
        public InventoryItemName(string value) : base(value)
        {
        }
    }
}
