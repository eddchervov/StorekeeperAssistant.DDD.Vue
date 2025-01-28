using MediatR;

namespace StorekeeperAssistant.UseCases
{
    public interface ICommand<out TRequest> : IRequest<TRequest>
    {
    }
}
