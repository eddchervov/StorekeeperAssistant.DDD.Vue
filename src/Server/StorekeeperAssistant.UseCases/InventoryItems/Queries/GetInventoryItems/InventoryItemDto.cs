using System;

namespace StorekeeperAssistant.UseCases;

public sealed class InventoryItemDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
}
