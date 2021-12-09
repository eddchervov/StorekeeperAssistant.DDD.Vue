using MediatR;
using Microsoft.AspNetCore.Mvc;
using StorekeeperAssistant.UseCases.InventoryItems.Queries.GetInventoryItems;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StorekeeperAssistant.Web.Controllers
{
    [Route("api/inventory-items")]
    [ApiController]
    public class InventoryItemController : ControllerBase
    {
        private readonly ISender _sender;

        public InventoryItemController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        public async Task<IEnumerable<InventoryItemDto>> GetInventoryItems()
        {
            return await _sender.Send(new GetInventoryItemsQuery());
        }
    }
}
