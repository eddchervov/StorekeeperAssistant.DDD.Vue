using BuildingBlocks.Domain;

namespace StorekeeperAssistant.Domain.InventoryItemAggregate
{
    public class InventoryItem : Entity, IAggregateRoot
    {
#nullable disable
        InventoryItem() { }
#nullable enable

        public InventoryItem(InventoryItemId id, InventoryItemName name)
        {
            Id = id;
            Name = name;
        }

        public InventoryItemId Id { get; }
        public InventoryItemName Name { get; }

        public bool IsDeleted { get; }
    }
}
