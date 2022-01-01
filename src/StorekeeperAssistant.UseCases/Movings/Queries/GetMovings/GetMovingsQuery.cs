using MediatR;

namespace StorekeeperAssistant.UseCases.Movings.Queries.GetMovings
{
    public record GetMovingsQuery(int SkipCount, int TakeCount) : IRequest<GetMovingDto>;
}
