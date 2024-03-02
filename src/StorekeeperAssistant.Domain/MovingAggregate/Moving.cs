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
        public MovingId Id { get; }
        public DateTime TransferDate { get; }

        public DepartureWarehouseId? DepartureWarehouseId { get; }
        public ArrivalWarehouseId? ArrivalWarehouseId { get; }

        private List<MovingDetail> _movingDetails;
        public IReadOnlyCollection<MovingDetail> MovingDetails => _movingDetails;

        private List<WarehouseInventoryItem> _warehouseInventoryItems;
        public IReadOnlyCollection<WarehouseInventoryItem> WarehouseInventoryItems => _warehouseInventoryItems;

        public bool IsDeleted { get; }

        #region ctor
#nullable disable
        private Moving() { }
#nullable enable

        private Moving(
            MovingId id,
            DateTime transferDate,
            DepartureWarehouseId? departureWarehouseId,
            ArrivalWarehouseId? arrivalWarehouseId,
            IEnumerable<MovingDetail> movingDetails,
            IEnumerable<WarehouseInventoryItem> warehouseInventoryItems)
        {
            Id = id;
            TransferDate = transferDate;

            if (departureWarehouseId == null && arrivalWarehouseId == null)
                throw new ArgumentException("Одновременно склад отправления и склад прибытия не может быть пустым");

            if (departureWarehouseId == arrivalWarehouseId)
                throw new ArgumentException("Склад отправления не должен быть равен складу прибытия");

            DepartureWarehouseId = departureWarehouseId;
            ArrivalWarehouseId = arrivalWarehouseId;

            _movingDetails = movingDetails.Any() == false
                ? throw new ArgumentException("Moving must contain at least 1 MovingDetail", nameof(movingDetails))
                : movingDetails.ToList();

            if (_movingDetails.GroupBy(x => x.Id.Value).Select(x => x.Count()).Any(x => x > 1))
                throw new ArgumentException("В одном перемещении не могут быть две одинаковые номенклатуры");

            _warehouseInventoryItems = warehouseInventoryItems.Any() == false
                ? throw new ArgumentException("Moving must contain at least 1 WarehouseInventoryItems", nameof(warehouseInventoryItems))
                : warehouseInventoryItems.ToList();
        }
        #endregion

        public static Moving Create(
            MovingId id,
            DateTime transferDate,
            DepartureWarehouseId? departureWarehouseId,
            ArrivalWarehouseId? arrivalWarehouseId,
            IEnumerable<MovingDetail> movingDetails,
            IEnumerable<WarehouseInventoryItem> warehouseInventoryItems)
        {
            return new Moving(id, transferDate, departureWarehouseId, arrivalWarehouseId, movingDetails, warehouseInventoryItems);
        }
    }
}
