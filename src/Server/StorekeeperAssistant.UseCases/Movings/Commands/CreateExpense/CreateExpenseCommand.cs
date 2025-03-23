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

namespace StorekeeperAssistant.UseCases.Movings.Commands.CreateExpense;

public sealed record CreateExpenseDto(Guid DepartureWarehouseId, IEnumerable<AddInventoryItemDto> InventoryItems);

public sealed class CreateExpenseCommand : ICommand<Guid>
{
    public required Guid DepartureWarehouseId { get; set; }
    public required IEnumerable<AddInventoryItemDto> InventoryItems { get; set; }
}

public sealed class CreateExpenseCommandHandler(
    IMovingRepository movingRepository,
    IWarehouseRepository warehouseRepository,
    IWarehouseInventoryItemRepository warehouseInventoryItemRepository,
    IInventoryItemRepository inventoryItemRepository,
    WarehouseInventoryItemService warehouseInventoryItemService) : IRequestHandler<CreateExpenseCommand, Guid>
{
    private readonly IMovingRepository _movingRepository = movingRepository;
    private readonly IWarehouseRepository _warehouseRepository = warehouseRepository;
    private readonly IWarehouseInventoryItemRepository _warehouseInventoryItemRepository = warehouseInventoryItemRepository;
    private readonly IInventoryItemRepository _inventoryItemRepository = inventoryItemRepository;
    private readonly WarehouseInventoryItemService _warehouseInventoryItemService = warehouseInventoryItemService;

    public async Task<Guid> Handle(CreateExpenseCommand request, CancellationToken cancellationToken)
    {
        var warehouse = (await _warehouseRepository.GetById(new WarehouseId(request.DepartureWarehouseId)))
            ?? throw new ArgumentException($"Склад отправления с id={request.DepartureWarehouseId} не найден");

        var inventoryItemIds = request.InventoryItems.Distinct().Select(x => new InventoryItemId(x.Id));
        var inventoryItems = await _inventoryItemRepository.GetByIds(inventoryItemIds);
        var lastWarehouseInventoryItems = await LoadLastWarehouseInventoryItems(new WarehouseId(request.DepartureWarehouseId), inventoryItemIds);

        var (moving, warehouseInventoryItems) = new MovingExpense(_warehouseInventoryItemService).Create(
                request.InventoryItems.Select(x => new MovingExpense.InventoryItemDto(x.Id, x.Count)),
                inventoryItems,
                warehouse,
                lastWarehouseInventoryItems
            );

        _movingRepository.Add(moving);
        _warehouseInventoryItemRepository.AddRange(warehouseInventoryItems);

        return moving.Id.Value;
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