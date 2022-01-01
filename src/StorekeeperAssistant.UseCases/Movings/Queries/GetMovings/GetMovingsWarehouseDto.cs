using System;

namespace StorekeeperAssistant.UseCases.Movings.Queries.GetMovings
{
    public class GetMovingsWarehouseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
    }
}
