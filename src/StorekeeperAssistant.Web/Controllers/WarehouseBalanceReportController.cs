using MediatR;
using Microsoft.AspNetCore.Mvc;
using StorekeeperAssistant.UseCases.Movings.Queries.GetWarehouseBalanceReport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StorekeeperAssistant.Web.Controllers
{
    [Route("api/warehouse-balance-report")]
    [ApiController]
    public class WarehouseBalanceReportController : ControllerBase
    {
        private readonly ISender _sender;

        public WarehouseBalanceReportController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("{warehouseId}")]
        public async Task<IEnumerable<WarehouseInventoryItemDto>> Get([FromRoute] Guid warehouseId)
        {
            return await _sender.Send(new GetWarehouseBalanceReportQuery(warehouseId, DateTime.UtcNow));
        }
    }
}
