using BuildingBlocks.Domain;
using StorekeeperAssistant.Domain.InventoryItems;

namespace StorekeeperAssistant.Domain.Movings.MovingDetails;

public sealed class MovingDetail : Entity
{
    public MovingDetailId Id { get; init; }
    public MovingId MovingId { get; init; }
    public MovingDetailCount Count { get; init; }
    public InventoryItemId InventoryItemId { get; init; }

    #region ctor
#nullable disable
    MovingDetail() { }
#nullable enable

    private MovingDetail(
        MovingDetailId id,
        MovingId movingId,
        InventoryItemId inventoryItemId,
        MovingDetailCount count)
    {
        Id = id;
        MovingId = movingId;
        Count = count;
        InventoryItemId = inventoryItemId;
    }
    #endregion

    public static MovingDetail Create(MovingDetailId id, MovingId movingId, InventoryItemId inventoryItemId, MovingDetailCount count)
    {
        return new MovingDetail
        {
            Id = id,
            MovingId = movingId,
            InventoryItemId = inventoryItemId,
            Count = count,
        };
    }
}