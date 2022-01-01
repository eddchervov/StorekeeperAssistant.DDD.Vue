using MediatR;
using System.Collections.Generic;

namespace StorekeeperAssistant.UseCases.InventoryItems.Queries.GetInventoryItems
{
    public record GetInventoryItemsQuery(): IRequest<IEnumerable<InventoryItemDto>>;
}
