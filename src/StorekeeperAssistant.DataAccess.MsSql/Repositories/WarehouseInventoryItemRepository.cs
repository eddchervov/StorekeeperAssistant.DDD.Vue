using Microsoft.EntityFrameworkCore;
using StorekeeperAssistant.Domain.InventoryItems;
using StorekeeperAssistant.Domain.Movings.WarehouseInventoryItems;
using StorekeeperAssistant.Domain.Warehouses;
using StorekeeperAssistant.UseCases.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace StorekeeperAssistant.DataAccess.Repositories;

public sealed class WarehouseInventoryItemRepository : IWarehouseInventoryItemRepository
{
    private readonly AppDbContext _context;

    public WarehouseInventoryItemRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<WarehouseInventoryItem?> Get(WarehouseId warehouseId, InventoryItemId inventoryItemId)
    {
        return _context.WarehouseInventoryItems
           .OrderByDescending(x => x.Date)
           .FirstOrDefaultAsync(x => x.WarehouseId == warehouseId && x.InventoryItemId == inventoryItemId);
    }
}
