using MediatR;
using Microsoft.AspNetCore.Mvc;
using StorekeeperAssistant.UseCases.Movings.Commands.AddMoving;
using StorekeeperAssistant.UseCases.Movings.Queries.GetMovings;
using System;
using System.Threading.Tasks;

namespace StorekeeperAssistant.Web.Controllers
{
    [Route("api/movings")]
    [ApiController]
    public class MovingController : ControllerBase
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
        public async Task<Guid> Add([FromBody] AddMovingDto addMovingDto)
        {
            return await _sender.Send(new AddMovingCommand(addMovingDto.DepartureWarehouseId, addMovingDto.ArrivalWarehouseId, addMovingDto.InventoryItems));
        }
    }
}
