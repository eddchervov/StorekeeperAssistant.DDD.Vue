using StorekeeperAssistant.Domain.InventoryItems;
using StorekeeperAssistant.Domain.WarehouseInventoryItems;
using StorekeeperAssistant.Domain.Warehouses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StorekeeperAssistant.UseCases.Interfaces;

public interface IWarehouseInventoryItemRepository
{
    void Add(WarehouseInventoryItem warehouseInventoryItem);
    void AddRange(IEnumerable<WarehouseInventoryItem> warehouseInventoryItems);
    Task<WarehouseInventoryItem?> GetLast(WarehouseId warehouseId, InventoryItemId inventoryItemId);
}
