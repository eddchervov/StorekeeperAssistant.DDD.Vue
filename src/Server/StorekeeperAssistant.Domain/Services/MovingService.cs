using StorekeeperAssistant.Domain.InventoryItems;
using StorekeeperAssistant.Domain.Movings;
using StorekeeperAssistant.Domain.Movings.MovingDetails;
using StorekeeperAssistant.Domain.WarehouseInventoryItems;
using StorekeeperAssistant.Domain.Warehouses;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StorekeeperAssistant.Domain.Services;

public sealed class MovingService
{
    private readonly WarehouseInventoryItemService _warehouseInventoryItemService;

    public MovingService(WarehouseInventoryItemService warehouseInventoryItemService)
    {
        _warehouseInventoryItemService = warehouseInventoryItemService;
    }

    public record InventoryItemDto(Guid Id, int Count);

    public (Moving Moving, IEnumerable<WarehouseInventoryItem> WarehouseInventoryItems) Create(
        IEnumerable<InventoryItemDto> inventoryItemDtos,
        IEnumerable<InventoryItem> inventoryItems,
        Warehouse departureWarehouse,
        Warehouse arrivalWarehouse,
        IEnumerable<WarehouseInventoryItem> lastWarehouseInventoryItems)
    {
        var movingId = new MovingId(Guid.NewGuid());
        var movingDetails = new List<MovingDetail>();
        var warehouseInventoryItems = new List<WarehouseInventoryItem>();
        var date = DateTime.UtcNow;

        foreach (var inventoryItemDto in inventoryItemDtos)
        {
            if (inventoryItems.Any(x => x.Id.Value == inventoryItemDto.Id) == false)
                throw new ArgumentException($"Номенклатура с id={inventoryItemDto.Id} не найдена");

            movingDetails.Add(CreateMovingDetail(movingId, inventoryItemDto));

            var departureWarehouseInventoryItem = _warehouseInventoryItemService.CreateExpenseWarehouseInventoryItem(
                movingId,
                departureWarehouse,
                lastWarehouseInventoryItems,
                date,
                new WarehouseInventoryItemService.InventoryItemDto(inventoryItemDto.Id, inventoryItemDto.Count));

            warehouseInventoryItems.Add(departureWarehouseInventoryItem);

            var arrivalWarehouseInventoryItem = _warehouseInventoryItemService.CreateIncomeWarehouseInventoryItem(
                movingId,
                arrivalWarehouse,
                lastWarehouseInventoryItems,
                date,
                new WarehouseInventoryItemService.InventoryItemDto(inventoryItemDto.Id, inventoryItemDto.Count));

            warehouseInventoryItems.Add(arrivalWarehouseInventoryItem);
        }

        var moving = CreateMoving(
            movingId,
            new DepartureWarehouseId(departureWarehouse.Id.Value),
            new ArrivalWarehouseId(arrivalWarehouse.Id.Value),
            date,
            movingDetails);

        return (moving, warehouseInventoryItems);
    }

    private static Moving CreateMoving(
        MovingId movingId,
        DepartureWarehouseId departureWarehouseId,
        ArrivalWarehouseId arrivalWarehouseId,
        DateTime date,
        IEnumerable<MovingDetail> movingDetails)
    {
        return Moving.CreateMoving(
            movingId,
            date,
            departureWarehouseId,
            arrivalWarehouseId,
            movingDetails
       );
    }

    private static MovingDetail CreateMovingDetail(MovingId movingId, InventoryItemDto inventoryItemDto)
    {
        return MovingDetail.Create(
                 new MovingDetailId(Guid.NewGuid()),
                 movingId,
                 new InventoryItemId(inventoryItemDto.Id),
                 new MovingDetailCount(inventoryItemDto.Count)
             );
    }
}
