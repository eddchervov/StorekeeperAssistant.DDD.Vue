using MediatR;
using Microsoft.AspNetCore.Mvc;
using StorekeeperAssistant.UseCases.Movings.Commands.CreateExpense;
using StorekeeperAssistant.UseCases.Movings.Commands.CreateIncome;
using StorekeeperAssistant.UseCases.Movings.Commands.CreateMoving;
using StorekeeperAssistant.UseCases.Movings.Queries.GetMovings;
using StorekeeperAssistant.UseCases.Movings.Queries.GetMovings.Dtos;
using System;
using System.Threading.Tasks;

namespace StorekeeperAssistant.Web.Controllers;

[Route("api/movings")]
[ApiController]
public sealed class MovingController : ControllerBase
{
    private readonly ISender _sender;

    public MovingController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("{skipCount}/{takeCount}")]
    public async Task<GetMovingDto> Get([FromRoute] int skipCount, int takeCount)
    {
        return await _sender.Send(new GetMovingsQuery(skipCount, takeCount));
    }

    [HttpPost("create-moving")]
    public async Task<Guid> CreateMoving([FromBody] CreateMovingDto request)
    {
        return await _sender.Send(new CreateMovingCommand
        {
            DepartureWarehouseId = request.DepartureWarehouseId,
            ArrivalWarehouseId = request.ArrivalWarehouseId,
            InventoryItems = request.InventoryItems
        });
    }

    [HttpPost("create-expense")]
    public async Task<Guid> CreateExpense([FromBody] CreateExpenseDto request)
    {
        return await _sender.Send(new CreateExpenseCommand
        {
            DepartureWarehouseId = request.DepartureWarehouseId,
            InventoryItems = request.InventoryItems
        });
    }

    [HttpPost("create-income")]
    public async Task<Guid> CreateIncome([FromBody] CreateIncomeDto request)
    {
        return await _sender.Send(new CreateIncomeCommand
        {
            ArrivalWarehouseId = request.ArrivalWarehouseId,
            InventoryItems = request.InventoryItems
        });
    }
}
