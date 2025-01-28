using AutoMapper;
using BuildingBlocks.Infrastructure;
using MediatR;
using StorekeeperAssistant.UseCases;

namespace StorekeeperAssistant.DataAccess;

public class SaveDataDecorator<TRequest, TResponse> : SaveDataDecoratorBase<TRequest, TResponse>
   where TRequest : ICommand<TResponse>
{
    public SaveDataDecorator(AppDbContext dbContext, IMediator mediator)
        : base(dbContext, mediator)
    { }
}
