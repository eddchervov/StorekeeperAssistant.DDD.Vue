using MediatR;
using System;
using System.Collections.Generic;

namespace StorekeeperAssistant.UseCases.Movings.Queries.GetWarehouseBalanceReport
{
    public record GetWarehouseBalanceReportQuery(Guid WarehouseId, DateTime? DateTime) : IRequest<IEnumerable<WarehouseInventoryItemDto>>;
}
