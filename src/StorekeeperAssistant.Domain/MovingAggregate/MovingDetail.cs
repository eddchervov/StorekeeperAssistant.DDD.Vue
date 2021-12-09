using BuildingBlocks.Domain;
using StorekeeperAssistant.Domain.InventoryItemAggregate;
using System.Collections.Generic;

namespace StorekeeperAssistant.Domain.MovingAggregate
{
    public class MovingDetail : ValueObject
    {
#nullable disable
        MovingDetail() { }
#nullable enable

        public MovingDetail(MovingDetailId id,
            MovingDetailCount count, 
            MovingId movingId, 
            InventoryItemId inventoryItemId)
        {
            Id = id;
            Count = count;
            MovingId = movingId;
            InventoryItemId = inventoryItemId;
        }

        public MovingDetailId Id { get; private set; } = default!;
        public MovingDetailCount Count { get; private set; }
        public MovingId MovingId { get; private set; }
        public InventoryItemId InventoryItemId { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Count;
            yield return MovingId;
            yield return InventoryItemId;
        }
    }
}
