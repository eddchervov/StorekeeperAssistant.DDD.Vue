using System.Collections.Generic;

namespace StorekeeperAssistant.UseCases.Movings.Queries.GetMovings.Dtos;

public sealed class GetMovingDto
{
    public int TotalCount { get; set; }
    public IEnumerable<MovingDto> Movings { get; set; } = new List<MovingDto>();
}
