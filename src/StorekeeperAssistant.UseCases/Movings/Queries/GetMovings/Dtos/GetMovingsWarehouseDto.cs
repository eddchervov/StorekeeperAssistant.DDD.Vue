using System;

namespace StorekeeperAssistant.UseCases.Movings.Queries.GetMovings.Dtos;

public sealed class GetMovingsWarehouseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
}
