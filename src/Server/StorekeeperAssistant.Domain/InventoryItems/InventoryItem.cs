using BuildingBlocks.Domain;

namespace StorekeeperAssistant.Domain.InventoryItems;

public sealed class InventoryItem : Entity, IAggregateRoot
{
    public InventoryItemId Id { get; init; }
    public InventoryItemName Name { get; init; }

    public bool IsDeleted { get; }

    #region ctor
#nullable disable
    InventoryItem() { }
#nullable enable

    private InventoryItem(InventoryItemId id, InventoryItemName name)
    {
        Id = id;
        Name = name;
    }
    #endregion

    public static InventoryItem Create(InventoryItemId id, InventoryItemName name)
    {
        return new InventoryItem
        {
            Id = id,
            Name = name
        };
    }
}
