using MediatR;
using System.Collections.Generic;

namespace StorekeeperAssistant.UseCases.InventoryItems.Queries.GetInventoryItems
{
    public class GetInventoryItemsQuery : IRequest<IEnumerable<InventoryItemDto>>
    {
    }
}
