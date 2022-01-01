using StorekeeperAssistant.Domain.WarehouseAggregate;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StorekeeperAssistant.UseCases.Interfaces
{
    public interface IWarehouseRepository
    {
        Task<IEnumerable<Warehouse>> GetByIds(IEnumerable<WarehouseId> warehouseIds);
        Task<Warehouse?> GetById(WarehouseId warehouseId);
    }
}
