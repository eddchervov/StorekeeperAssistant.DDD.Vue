using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace BuildingBlocks.Infrastructure;

public abstract class SaveDataDecoratorBase<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
      where TRequest : IRequest<TResponse>
{
    private readonly DbContext _context;
    private readonly IMediator _mediator;

    protected SaveDataDecoratorBase(DbContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var result = await next();

        await _context.SaveChangesAsync(cancellationToken);

        return result;
    }
}
