using MediatR;
using Microsoft.AspNetCore.Mvc;
using StorekeeperAssistant.UseCases.Warehouses.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StorekeeperAssistant.Web.Controllers
{
    [Route("api/warehouses")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        private readonly ISender _sender;

        public WarehouseController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        public async Task<IEnumerable<WarehouseDto>> GetWarehouses()
        {
            return await _sender.Send(new GetWarehousesQuery());
        }
    }
}
