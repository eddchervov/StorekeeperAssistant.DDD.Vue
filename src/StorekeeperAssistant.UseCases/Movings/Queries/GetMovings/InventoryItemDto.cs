using System;

namespace StorekeeperAssistant.UseCases.Movings.Queries.GetMovings
{
    public class InventoryItemDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
    }
}
