using System;

namespace StorekeeperAssistant.UseCases.Movings.Queries.GetMovings
{
    public class WarehouseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
    }
}
