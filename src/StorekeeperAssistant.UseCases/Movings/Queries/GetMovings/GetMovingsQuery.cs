using MediatR;
using System.Collections.Generic;

namespace StorekeeperAssistant.UseCases.Movings.Queries.GetMovings
{
    public class GetMovingsQuery : IRequest<IEnumerable<MovingDto>>
    {
    }
}
