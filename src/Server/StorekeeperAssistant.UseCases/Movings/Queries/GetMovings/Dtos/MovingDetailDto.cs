using System;

namespace StorekeeperAssistant.UseCases.Movings.Queries.GetMovings.Dtos;

public sealed class MovingDetailDto
{
    public Guid Id { get; set; }
    public InventoryItemDto InventoryItem { get; set; } = default!;
    public int Count { get; set; }
}
