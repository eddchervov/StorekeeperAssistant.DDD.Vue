using BuildingBlocks.Domain;
using StorekeeperAssistant.Domain.WarehouseAggregate;
using System;
using System.Collections.Generic;

namespace StorekeeperAssistant.Domain.MovingAggregate
{
    public class Moving : Entity, IAggregateRoot
    {
        private Moving()
        {
            _movingDetails = new List<MovingDetail>();
        }

        public Moving(MovingId id,
            List<MovingDetail> movingDetails,
            WarehouseId? departureWarehouseId,
            WarehouseId? arrivalWarehouseId)
        {
            Id = id;
            _movingDetails = movingDetails.Count == 0 
                ? throw new ArgumentException("Moving must contain at least 1 MovingDetail", nameof(movingDetails)) 
                : movingDetails;
            TransferDate = DateTime.UtcNow;
            DepartureWarehouseId = departureWarehouseId;
            ArrivalWarehouseId = arrivalWarehouseId;
        }

        private List<MovingDetail> _movingDetails;
        public IEnumerable<MovingDetail> MovingDetails => _movingDetails;

        public MovingId Id { get; private set; } = default!;
        public DateTime TransferDate { get; private set; }

        public WarehouseId? DepartureWarehouseId { get; private set; }
        public WarehouseId? ArrivalWarehouseId { get; private set; }

        public bool IsDeleted { get; private set; }
    }
}
