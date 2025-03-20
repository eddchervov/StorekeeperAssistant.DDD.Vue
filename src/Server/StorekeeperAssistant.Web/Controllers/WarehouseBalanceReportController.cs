using MediatR;
using Microsoft.AspNetCore.Mvc;
using StorekeeperAssistant.UseCases.Movings.Queries.GetWarehouseBalanceReport;
using StorekeeperAssistant.UseCases.Movings.Queries.GetWarehouseBalanceReport.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StorekeeperAssistant.Web.Controllers;

[Route("api/warehouse-balance-report")]
[ApiController]
public sealed class WarehouseBalanceReportController : ControllerBase
{
    private readonly ISender _sender;

    public WarehouseBalanceReportController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("{warehouseId}/{maxDateTime?}")]
    public async Task<IEnumerable<WarehouseInventoryItemDto>> Get([FromRoute] Guid warehouseId, DateTime? maxDateTime)
    {
        return await _sender.Send(new GetWarehouseBalanceReportQuery(warehouseId, maxDateTime ?? DateTime.UtcNow));
    }
}
