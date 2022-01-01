using System;

namespace StorekeeperAssistant.UseCases.Movings.Commands.AddMoving
{
    public class AddInventoryItemDto
    {
        public Guid Id { get; set; }
        public int Count { get; set; }
    }
}
