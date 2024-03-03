using System;

namespace StorekeeperAssistant.UseCases.Warehouses.Queries.Dtos;

public sealed class WarehouseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
}
