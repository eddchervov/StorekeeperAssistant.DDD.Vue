using System;

namespace StorekeeperAssistant.UseCases.Movings.Commands.Common;

public sealed class AddInventoryItemDto
{
    public Guid Id { get; set; }
    public int Count { get; set; }
}
