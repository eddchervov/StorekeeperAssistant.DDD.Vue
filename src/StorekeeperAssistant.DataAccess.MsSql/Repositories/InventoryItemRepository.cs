using Microsoft.EntityFrameworkCore;
using StorekeeperAssistant.Domain.InventoryItemAggregate;
using StorekeeperAssistant.UseCases.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StorekeeperAssistant.DataAccess.Repositories
{
    public class InventoryItemRepository : IInventoryItemRepository
    {
        private readonly AppDbContext _context;

        public InventoryItemRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<InventoryItem>> GetByIds(IEnumerable<InventoryItemId> inventoryItemIds)
        {
            return await _context.InventoryItems.Where(x => inventoryItemIds.Contains(x.Id) && x.IsDeleted == false).ToListAsync();
        }
    }
}
