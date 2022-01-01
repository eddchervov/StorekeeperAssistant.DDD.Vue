using Microsoft.EntityFrameworkCore;
using StorekeeperAssistant.Domain.InventoryItemAggregate;
using StorekeeperAssistant.Domain.MovingAggregate.WarehouseInventoryItems;
using StorekeeperAssistant.Domain.WarehouseAggregate;
using StorekeeperAssistant.UseCases.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace StorekeeperAssistant.DataAccess.Repositories
{
    public class WarehouseInventoryItemRepository : IWarehouseInventoryItemRepository
    {
        private readonly AppDbContext _context;

        public WarehouseInventoryItemRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<WarehouseInventoryItem> Get(WarehouseId warehouseId, InventoryItemId inventoryItemId)
        {
            return await _context.WarehouseInventoryItems
               .OrderByDescending(x => x.Date)
               .FirstOrDefaultAsync(x => x.WarehouseId == warehouseId && x.InventoryItemId == inventoryItemId);
        }
    }
}
