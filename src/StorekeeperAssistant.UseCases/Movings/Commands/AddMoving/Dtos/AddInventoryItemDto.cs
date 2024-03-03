using System;

namespace StorekeeperAssistant.UseCases.Movings.Commands.AddMoving.Dtos;

public sealed class AddInventoryItemDto
{
    public Guid Id { get; set; }
    public int Count { get; set; }
}
