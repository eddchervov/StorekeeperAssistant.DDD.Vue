using MediatR;
using StorekeeperAssistant.Domain.InventoryItems;
using StorekeeperAssistant.Domain.Services;
using StorekeeperAssistant.Domain.WarehouseInventoryItems;
using StorekeeperAssistant.Domain.Warehouses;
using StorekeeperAssistant.UseCases.Interfaces;
using StorekeeperAssistant.UseCases.Movings.Commands.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StorekeeperAssistant.UseCases.Movings.Commands.CreateMoving;

public sealed record CreateMovingDto(Guid DepartureWarehouseId, Guid ArrivalWarehouseId, IEnumerable<AddInventoryItemDto> InventoryItems);

public sealed class CreateMovingCommand : ICommand<Guid>
{
    public required Guid DepartureWarehouseId { get; set; }
    public required Guid ArrivalWarehouseId { get; set; }
    public required IEnumerable<AddInventoryItemDto> InventoryItems { get; set; }
}

public sealed class CreateMovingCommandHandler : IRequestHandler<CreateMovingCommand, Guid>
{
    private readonly IMovingRepository _movingRepository;
    private readonly IWarehouseRepository _warehouseRepository;
    private readonly IWarehouseInventoryItemRepository _warehouseInventoryItemRepository;
    private readonly IInventoryItemRepository _inventoryItemRepository;
    private readonly WarehouseInventoryItemService _warehouseInventoryItemService;

    public CreateMovingCommandHandler(
        IMovingRepository movingRepository,
        IWarehouseRepository warehouseRepository,
        IWarehouseInventoryItemRepository warehouseInventoryItemRepository,
        IInventoryItemRepository inventoryItemRepository,
        WarehouseInventoryItemService warehouseInventoryItemService)
    {
        _movingRepository = movingRepository;
        _warehouseRepository = warehouseRepository;
        _warehouseInventoryItemRepository = warehouseInventoryItemRepository;
        _inventoryItemRepository = inventoryItemRepository;
        _warehouseInventoryItemService = warehouseInventoryItemService;
    }

    public async Task<Guid> Handle(CreateMovingCommand request, CancellationToken cancellationToken)
    {
        var departureWarehouse = (await _warehouseRepository.GetById(new WarehouseId(request.DepartureWarehouseId)))
            ?? throw new ArgumentException($"Склад отправления с id={request.DepartureWarehouseId} не найден");

        var arrivalWarehouse = (await _warehouseRepository.GetById(new WarehouseId(request.ArrivalWarehouseId)))
            ?? throw new ArgumentException($"Склад прибытия с id={request.ArrivalWarehouseId} не найден");

        var inventoryItemIds = request.InventoryItems.Distinct().Select(x => new InventoryItemId(x.Id));
        var inventoryItems = await _inventoryItemRepository.GetByIds(inventoryItemIds);
        var lastWarehouseInventoryItems = await LoadLastWarehouseInventoryItems(request.DepartureWarehouseId, request.ArrivalWarehouseId, inventoryItemIds);

        var (moving, warehouseInventoryItems) = new MovingService(_warehouseInventoryItemService).Create(
                request.InventoryItems.Select(x => new MovingService.InventoryItemDto(x.Id, x.Count)),
                inventoryItems,
                departureWarehouse,
                arrivalWarehouse,
                lastWarehouseInventoryItems
            );

        _movingRepository.Add(moving);
        _warehouseInventoryItemRepository.AddRange(warehouseInventoryItems);

        return moving.Id.Value;
    }

    private async Task<IEnumerable<WarehouseInventoryItem>> LoadLastWarehouseInventoryItems(
        Guid departureWarehouseId,
        Guid arrivalWarehouseId, 
        IEnumerable<InventoryItemId> inventoryItemIds)
    {
        var result = new List<WarehouseInventoryItem>();

        result.AddRange(await LoadLastWarehouseInventoryItems(new WarehouseId(departureWarehouseId), inventoryItemIds));
        result.AddRange(await LoadLastWarehouseInventoryItems(new WarehouseId(arrivalWarehouseId), inventoryItemIds));

        return result;
    }

    private async Task<IEnumerable<WarehouseInventoryItem>> LoadLastWarehouseInventoryItems(WarehouseId warehouseId, IEnumerable<InventoryItemId> inventoryItemIds)
    {
        var warehouseInventoryItems = new List<WarehouseInventoryItem>();

        foreach (var inventoryItemId in inventoryItemIds)
        {
            var warehouseInventoryItem = await _warehouseInventoryItemRepository.GetLast(warehouseId, inventoryItemId);
            if (warehouseInventoryItem != null)
                warehouseInventoryItems.Add(warehouseInventoryItem);
        }
        return warehouseInventoryItems;
    }
}