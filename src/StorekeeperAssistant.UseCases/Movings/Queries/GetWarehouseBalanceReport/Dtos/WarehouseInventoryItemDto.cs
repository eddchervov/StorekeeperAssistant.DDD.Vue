using System;

namespace StorekeeperAssistant.UseCases.Movings.Queries.GetWarehouseBalanceReport.Dtos;

public sealed class WarehouseInventoryItemDto
{
    public Guid Id { get; set; }
    public int Count { get; set; }
    public InventoryItemDto InventoryItem { get; set; } = default!;
}
