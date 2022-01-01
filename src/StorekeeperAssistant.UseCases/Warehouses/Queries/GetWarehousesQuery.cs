using MediatR;
using System.Collections.Generic;

namespace StorekeeperAssistant.UseCases.Warehouses.Queries
{
    public record GetWarehousesQuery(): IRequest<IEnumerable<WarehouseDto>>;
}
