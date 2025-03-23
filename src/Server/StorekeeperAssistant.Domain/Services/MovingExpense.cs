using StorekeeperAssistant.Domain.InventoryItems;
using StorekeeperAssistant.Domain.Movings;
using StorekeeperAssistant.Domain.Movings.MovingDetails;
using StorekeeperAssistant.Domain.WarehouseInventoryItems;
using StorekeeperAssistant.Domain.Warehouses;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StorekeeperAssistant.Domain.Services;

public sealed class MovingExpense
{
    private readonly WarehouseInventoryItemService _warehouseInventoryItemService;

    public MovingExpense(WarehouseInventoryItemService warehouseInventoryItemService)
    {
        _warehouseInventoryItemService = warehouseInventoryItemService;
    }

    public record InventoryItemDto(Guid Id, int Count);

    public (Moving Moving, IEnumerable<WarehouseInventoryItem> WarehouseInventoryItems) Create(
        IEnumerable<InventoryItemDto> inventoryItemDtos,
        IEnumerable<InventoryItem> inventoryItems,
        Warehouse warehouse,
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

            var warehouseInventoryItem = _warehouseInventoryItemService.CreateExpenseWarehouseInventoryItem(
                movingId,
                warehouse,
                lastWarehouseInventoryItems,
                date,
                new WarehouseInventoryItemService.InventoryItemDto(inventoryItemDto.Id, inventoryItemDto.Count));

            warehouseInventoryItems.Add(warehouseInventoryItem);
        }

        var moving = CreateMoving(movingId, warehouse, date, movingDetails);

        return (moving, warehouseInventoryItems);
    }

    private static Moving CreateMoving(MovingId movingId, Warehouse warehouse, DateTime date, IEnumerable<MovingDetail> movingDetails)
    {
        return Moving.CreateExpense(
            movingId,
            date,
            new DepartureWarehouseId(warehouse.Id.Value),
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
