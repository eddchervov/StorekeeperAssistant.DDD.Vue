using StorekeeperAssistant.Domain.InventoryItemAggregate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StorekeeperAssistant.UseCases.Interfaces
{
    public interface IInventoryItemRepository
    {
        Task<IEnumerable<InventoryItem>> GetByIds(IEnumerable<InventoryItemId> inventoryItemIds);
    }
}
