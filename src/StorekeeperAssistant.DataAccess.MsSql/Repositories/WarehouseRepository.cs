using Microsoft.EntityFrameworkCore;
using StorekeeperAssistant.Domain.WarehouseAggregate;
using StorekeeperAssistant.UseCases.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StorekeeperAssistant.DataAccess.Repositories
{
    public class WarehouseRepository : IWarehouseRepository
    {
        private readonly AppDbContext _context;

        public WarehouseRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Warehouse>> GetByIds(IEnumerable<WarehouseId> warehouseIds)
        {
            return await _context.Warehouses.Where(x => warehouseIds.Contains(x.Id) && x.IsDeleted == false).ToListAsync();
        }

        public async Task<Warehouse?> GetById(WarehouseId warehouseId)
        {
            return await _context.Warehouses.FirstOrDefaultAsync(x => x.Id == warehouseId && x.IsDeleted == false);
        }
    }
}
