using MediatR;
using Microsoft.AspNetCore.Mvc;
using StorekeeperAssistant.UseCases.Movings.Commands.AddMoving;
using StorekeeperAssistant.UseCases.Movings.Commands.AddMoving.Dtos;
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

    [HttpPost]
    public async Task<Guid> Add([FromBody] AddMovingDto request)
    {
        return await _sender.Send(new AddMovingCommand
        {
            DepartureWarehouseId = request.DepartureWarehouseId,
            ArrivalWarehouseId = request.ArrivalWarehouseId,
            InventoryItems = request.InventoryItems
        });
    }
}
