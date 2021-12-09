using System;

namespace StorekeeperAssistant.UseCases.Movings.Queries.GetMovings
{
    public class MovingDetailDto
    {
        public Guid Id { get; set; }
        public InventoryItemDto InventoryItem { get; set; } = default!;
        public int Count { get; set; }
    }
}
