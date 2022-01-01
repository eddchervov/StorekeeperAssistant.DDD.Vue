using System;

namespace StorekeeperAssistant.UseCases
{
    public class InventoryItemDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
    }
}
