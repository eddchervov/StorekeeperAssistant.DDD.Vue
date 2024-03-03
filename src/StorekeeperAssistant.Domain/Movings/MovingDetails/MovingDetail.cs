using BuildingBlocks.Domain;
using StorekeeperAssistant.Domain.InventoryItems;

namespace StorekeeperAssistant.Domain.Movings.MovingDetails;

public sealed class MovingDetail : Entity
{
#nullable disable
    MovingDetail() { }
#nullable enable

    public MovingDetail(MovingDetailId id,
        MovingId movingId,
        InventoryItemId inventoryItemId,
        MovingDetailCount count)
    {
        Id = id;
        MovingId = movingId;
        InventoryItemId = inventoryItemId;
        Count = count;
    }

    public MovingDetailId Id { get; }
    public MovingId MovingId { get; }
    public MovingDetailCount Count { get; }
    public InventoryItemId InventoryItemId { get; }
}
