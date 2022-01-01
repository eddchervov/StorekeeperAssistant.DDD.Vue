using StorekeeperAssistant.Domain.InventoryItemAggregate;
using StorekeeperAssistant.Domain.MovingAggregate.WarehouseInventoryItems;
using StorekeeperAssistant.Domain.WarehouseAggregate;
using System.Threading.Tasks;

namespace StorekeeperAssistant.UseCases.Interfaces
{
    public interface IWarehouseInventoryItemRepository
    {
        Task<WarehouseInventoryItem> Get(WarehouseId warehouseId, InventoryItemId inventoryItemId);
    }
}
