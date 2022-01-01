using MediatR;
using StorekeeperAssistant.Domain.WarehouseAggregate;
using System;
using System.Collections.Generic;

namespace StorekeeperAssistant.UseCases.Movings.Commands.AddMoving
{
    public class AddMovingCommand : IRequest<Guid>
    {
        public AddMovingCommand(Guid? departureWarehouseId, Guid? arrivalWarehouseId, List<AddInventoryItemDto> inventoryItems)
        {
            DepartureWarehouseId = departureWarehouseId;
            ArrivalWarehouseId = arrivalWarehouseId;

            if (DepartureWarehouseId == null && ArrivalWarehouseId == null)
                throw new ArgumentException("Одновременно склад отправления и склад прибытия не может быть пустым");

            InventoryItems = inventoryItems;
        }

        public Guid? DepartureWarehouseId { get; }
        public Guid? ArrivalWarehouseId { get; }
        public IEnumerable<AddInventoryItemDto> InventoryItems { get; }

        public IEnumerable<WarehouseId> GetWarehouseIds()
        {
            var warehouseIds = new List<WarehouseId>();

            if (DepartureWarehouseId != null) warehouseIds.Add(new WarehouseId(DepartureWarehouseId.Value));
            if (ArrivalWarehouseId != null) warehouseIds.Add(new WarehouseId(ArrivalWarehouseId.Value));

            return warehouseIds;
        }
    }
}
