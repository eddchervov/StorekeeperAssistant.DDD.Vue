using System;

namespace StorekeeperAssistant.UseCases.Warehouses.Queries
{
    public class WarehouseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
    }
}
