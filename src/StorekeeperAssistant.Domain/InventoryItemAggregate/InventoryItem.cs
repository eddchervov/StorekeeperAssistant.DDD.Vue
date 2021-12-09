namespace StorekeeperAssistant.Domain.InventoryItemAggregate
{
    public class InventoryItem
    {
#nullable disable
        InventoryItem() { }
#nullable enable

        public InventoryItem(InventoryItemId id, InventoryItemName name)
        {
            Id = id;
            Name = name;
        }

        public InventoryItemId Id { get; private set; }
        public InventoryItemName Name { get; private set; }

        public bool IsDeleted { get; private set; }
    }
}
