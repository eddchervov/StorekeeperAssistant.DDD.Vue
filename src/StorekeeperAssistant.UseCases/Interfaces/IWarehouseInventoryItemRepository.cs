using StorekeeperAssistant.Domain.InventoryItems;
using StorekeeperAssistant.Domain.Movings.WarehouseInventoryItems;
using StorekeeperAssistant.Domain.Warehouses;
using System.Threading.Tasks;

namespace StorekeeperAssistant.UseCases.Interfaces;

public interface IWarehouseInventoryItemRepository
{
    Task<WarehouseInventoryItem?> Get(WarehouseId warehouseId, InventoryItemId inventoryItemId);
}
