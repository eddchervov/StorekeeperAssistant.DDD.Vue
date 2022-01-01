using BuildingBlocks.Domain;
using StorekeeperAssistant.Domain.MovingAggregate.MovingDetails;
using StorekeeperAssistant.Domain.MovingAggregate.WarehouseInventoryItems;
using StorekeeperAssistant.Domain.WarehouseAggregate;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StorekeeperAssistant.Domain.MovingAggregate
{
    public class Moving : Entity, IAggregateRoot
    {
#nullable disable
        Moving() { }
#nullable enable

        public Moving(MovingId id,
            List<MovingDetail> movingDetails,
            List<WarehouseInventoryItem> warehouseInventoryItems,
            DateTime transferDate,
            WarehouseId? departureWarehouseId,
            WarehouseId? arrivalWarehouseId)
        {
            Id = id;
            TransferDate = transferDate;

            if (departureWarehouseId == null && arrivalWarehouseId == null)
                throw new ArgumentException("Одновременно склад отправления и склад прибытия не может быть пустым");

            if (departureWarehouseId == arrivalWarehouseId)
                throw new ArgumentException("Склад отправления не должен быть равен складу прибытия");

            DepartureWarehouseId = departureWarehouseId;
            ArrivalWarehouseId = arrivalWarehouseId;

            _movingDetails = movingDetails.Count == 0
                ? throw new ArgumentException("Moving must contain at least 1 MovingDetail", nameof(movingDetails))
                : movingDetails;

            if (_movingDetails.GroupBy(x => x.Id.Value).Select(x => x.Count()).Any(x => x > 1))
                throw new ArgumentException("В одном перемещении не могут быть две одинаковые номенклатуры");

            _warehouseInventoryItems = warehouseInventoryItems.Count == 0
                ? throw new ArgumentException("Moving must contain at least 1 WarehouseInventoryItems", nameof(warehouseInventoryItems))
                : warehouseInventoryItems;
        }

        public MovingId Id { get; }
        public DateTime TransferDate { get; }

        public WarehouseId? DepartureWarehouseId { get; }
        public WarehouseId? ArrivalWarehouseId { get; }

        private List<MovingDetail> _movingDetails;
        public IEnumerable<MovingDetail> MovingDetails => _movingDetails;

        private List<WarehouseInventoryItem> _warehouseInventoryItems;
        public IEnumerable<WarehouseInventoryItem> WarehouseInventoryItems => _warehouseInventoryItems;

        public bool IsDeleted { get; }
    }
}
