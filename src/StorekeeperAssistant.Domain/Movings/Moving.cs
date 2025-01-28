using BuildingBlocks.Domain;
using StorekeeperAssistant.Domain.Movings.MovingDetails;
using StorekeeperAssistant.Domain.Warehouses;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StorekeeperAssistant.Domain.Movings;

public sealed class Moving : Entity, IAggregateRoot
{
    public MovingId Id { get; init; }

    public DateTime TransferDate { get; init; }

    public DepartureWarehouseId? DepartureWarehouseId { get; init; }
    public ArrivalWarehouseId? ArrivalWarehouseId { get; init; }

    public MovementType MovementType { get; init; }

    private List<MovingDetail> _movingDetails;
    public IReadOnlyCollection<MovingDetail> MovingDetails => _movingDetails;

    public bool IsDeleted { get; }

    #region ctor
#nullable disable
    Moving() { }
#nullable enable
    #endregion

    public static Moving CreateIncome(
        MovingId id,
        DateTime transferDate,
        ArrivalWarehouseId arrivalWarehouseId,
        IEnumerable<MovingDetail> movingDetails)
    {
        ValidationMovingDetails(movingDetails);

        return new Moving
        {
            Id = id,
            TransferDate = transferDate,
            ArrivalWarehouseId = arrivalWarehouseId,
            DepartureWarehouseId = null,
            MovementType = MovementType.Income,
            _movingDetails = movingDetails.ToList()
        };
    }

    public static Moving CreateExpense(
        MovingId id,
        DateTime transferDate,
        DepartureWarehouseId departureWarehouseId,
        IEnumerable<MovingDetail> movingDetails)
    {
        ValidationMovingDetails(movingDetails);

        return new Moving
        {
            Id = id,
            TransferDate = transferDate,
            ArrivalWarehouseId = null,
            DepartureWarehouseId = departureWarehouseId,
            MovementType = MovementType.Expense,
            _movingDetails = movingDetails.ToList()
        };
    }

    public static Moving CreateMoving(
        MovingId id,
        DateTime transferDate,
        DepartureWarehouseId departureWarehouseId,
        ArrivalWarehouseId arrivalWarehouseId,
        IEnumerable<MovingDetail> movingDetails)
    {
        ValidationMovingDetails(movingDetails);

        return new Moving
        {
            Id = id,
            TransferDate = transferDate,
            DepartureWarehouseId = departureWarehouseId,
            ArrivalWarehouseId = arrivalWarehouseId,
            MovementType = MovementType.Moving,
            _movingDetails = movingDetails.ToList()
        };
    }

    private static void ValidationMovingDetails(IEnumerable<MovingDetail> movingDetails)
    {
        if (movingDetails.Any() == false)
            throw new ArgumentException("Moving must contain at least 1 MovingDetail", nameof(movingDetails));

        if (movingDetails.GroupBy(x => x.Id.Value).Select(x => x.Count()).Any(x => x > 1))
            throw new ArgumentException("В одном перемещении не могут быть две одинаковые номенклатуры");
    }
}
