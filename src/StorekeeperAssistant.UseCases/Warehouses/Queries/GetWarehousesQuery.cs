using MediatR;
using System.Collections.Generic;

namespace StorekeeperAssistant.UseCases.Warehouses.Queries
{
    public class GetWarehousesQuery : IRequest<IEnumerable<WarehouseDto>>
    {
    }
}
