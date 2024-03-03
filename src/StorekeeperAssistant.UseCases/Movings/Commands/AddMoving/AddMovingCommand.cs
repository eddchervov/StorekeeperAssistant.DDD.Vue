using MediatR;
using StorekeeperAssistant.Domain.InventoryItems;
using StorekeeperAssistant.Domain.Movings;
using StorekeeperAssistant.Domain.Movings.MovingDetails;
using StorekeeperAssistant.Domain.Movings.WarehouseInventoryItems;
using StorekeeperAssistant.Domain.Warehouses;
using StorekeeperAssistant.UseCases.Interfaces;
using StorekeeperAssistant.UseCases.Movings.Commands.AddMoving.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StorekeeperAssistant.UseCases.Movings.Commands.AddMoving;

public sealed class AddMovingCommand : IRequest<Guid>
{
    public Guid? DepartureWarehouseId { get; set; }
    public Guid? ArrivalWarehouseId { get; set; }
    public IEnumerable<AddInventoryItemDto> InventoryItems { get; set; } = new List<AddInventoryItemDto>();
}

public sealed class AddMovingCommandHandler : IRequestHandler<AddMovingCommand, Guid>
{
    private readonly IMovingRepository _movingRepository;
    private readonly IWarehouseRepository _warehouseRepository;
    private readonly IWarehouseInventoryItemRepository _warehouseInventoryItemRepository;
    private readonly IInventoryItemRepository _inventoryItemRepository;

    public AddMovingCommandHandler(IMovingRepository movingRepository,
        IWarehouseRepository warehouseRepository,
        IWarehouseInventoryItemRepository warehouseInventoryItemRepository,
        IInventoryItemRepository inventoryItemRepository)
    {
        _movingRepository = movingRepository;
        _warehouseRepository = warehouseRepository;
        _warehouseInventoryItemRepository = warehouseInventoryItemRepository;
        _inventoryItemRepository = inventoryItemRepository;
    }

    private readonly DateTime _utcNow = DateTime.UtcNow;
    private readonly MovingId _movingId = new(Guid.NewGuid());
    private List<MovingDetail> _movingDetails = new();
    private List<WarehouseInventoryItem> _warehouseInventoryItems = new();

    public async Task<Guid> Handle(AddMovingCommand request, CancellationToken cancellationToken)
    {
        var warehouses = await _warehouseRepository.GetByIds(GetWarehouseIds(request.DepartureWarehouseId, request.ArrivalWarehouseId));

        if (request.DepartureWarehouseId != null && warehouses.Any(x => x.Id.Value == request.DepartureWarehouseId) == false)
            throw new ArgumentException($"Склад отправлени с id={request.DepartureWarehouseId.Value} не найден");

        if (request.ArrivalWarehouseId != null && warehouses.Any(x => x.Id.Value == request.ArrivalWarehouseId) == false)
            throw new ArgumentException($"Склад прибытия с id={request.ArrivalWarehouseId.Value} не найден");

        var inventoryItems = await _inventoryItemRepository.GetByIds(request.InventoryItems.Distinct().Select(x => new InventoryItemId(x.Id)));

        foreach (var inventoryItem in request.InventoryItems)
        {
            if (inventoryItems.Any(x => x.Id.Value == inventoryItem.Id) == false)
                throw new ArgumentException($"Номенклатура с id={inventoryItem.Id} не найдена");

            _movingDetails.Add(CreateMovingDetail(inventoryItem));

            if (request.DepartureWarehouseId != null)
            {
                var warehouseInventoryItem = await CreateDepartureWarehouseInventoryItem(inventoryItem, request.DepartureWarehouseId.Value);
                _warehouseInventoryItems.Add(warehouseInventoryItem);
            }

            if (request.ArrivalWarehouseId != null)
            {
                var warehouseInventoryItem = await CreateArrivalWarehouseInventoryItem(inventoryItem, request.ArrivalWarehouseId.Value);
                _warehouseInventoryItems.Add(warehouseInventoryItem);
            }
        }

        _movingRepository.Add(CreateMoving(
                request.DepartureWarehouseId != null ? new DepartureWarehouseId(request.DepartureWarehouseId.Value) : null,
                request.ArrivalWarehouseId != null ? new ArrivalWarehouseId(request.ArrivalWarehouseId.Value) : null
            ));

        await _movingRepository.SaveAsync();

        return _movingId.Value;
    }

    private static IEnumerable<WarehouseId> GetWarehouseIds(Guid? departureWarehouseId, Guid? arrivalWarehouseId)
    {
        var warehouseIds = new List<WarehouseId>();

        if (departureWarehouseId != null) warehouseIds.Add(new WarehouseId(departureWarehouseId.Value));
        if (arrivalWarehouseId != null) warehouseIds.Add(new WarehouseId(arrivalWarehouseId.Value));

        return warehouseIds;
    }

    private Moving CreateMoving(DepartureWarehouseId? departureWarehouseId, ArrivalWarehouseId? arrivalWarehouseId)
    {
        return Moving.Create(
                _movingId,
                _utcNow,
                departureWarehouseId,
                arrivalWarehouseId,
                 _movingDetails,
                _warehouseInventoryItems
            );
    }

    private async Task<WarehouseInventoryItem> CreateArrivalWarehouseInventoryItem(AddInventoryItemDto addInventoryItemDto, Guid arrivalWarehouseId)
    {
        var inventoryItemId = new InventoryItemId(addInventoryItemDto.Id);
        var warehouseId = new WarehouseId(arrivalWarehouseId);

        var warehouseInventoryItemCount = addInventoryItemDto.Count;
        var lastArrivalWarehouseInventoryItem = await GetLastWarehouseInventoryItem(warehouseId, inventoryItemId);
        if (lastArrivalWarehouseInventoryItem != null) warehouseInventoryItemCount += lastArrivalWarehouseInventoryItem.Count.Value;

        return new WarehouseInventoryItem(
            new WarehouseInventoryItemId(Guid.NewGuid()),
                _movingId,
                inventoryItemId,
                warehouseId,
                new WarehouseInventoryItemCount(warehouseInventoryItemCount)
            );
    }

    private async Task<WarehouseInventoryItem> CreateDepartureWarehouseInventoryItem(AddInventoryItemDto addInventoryItemDto, Guid departureWarehouseId)
    {
        var inventoryItemId = new InventoryItemId(addInventoryItemDto.Id);
        var warehouseId = new WarehouseId(departureWarehouseId);

        var lastDepartureWarehouseInventoryItem = await GetLastWarehouseInventoryItem(warehouseId, inventoryItemId);
        if (lastDepartureWarehouseInventoryItem == null)
            throw new ArgumentException($"Склад отправления с Id={departureWarehouseId} и номенклатурой Id={addInventoryItemDto.Id} не найден");

        var warehouseInventoryItemCount = lastDepartureWarehouseInventoryItem.Count.Value - addInventoryItemDto.Count;
        if (warehouseInventoryItemCount < 0)
            throw new ArgumentException($"Нельзя расходовать номенклатуру id={addInventoryItemDto.Id} в кол-ве: {addInventoryItemDto.Count}. " +
                $"Недостаточно остатков на складе, остаток: {lastDepartureWarehouseInventoryItem.Count}");

        return new WarehouseInventoryItem(
                new WarehouseInventoryItemId(Guid.NewGuid()),
                _movingId,
                inventoryItemId,
                warehouseId,
                new WarehouseInventoryItemCount(warehouseInventoryItemCount)
            );
    }

    private MovingDetail CreateMovingDetail(AddInventoryItemDto addInventoryItemDto)
    {
        return new MovingDetail(
                 new MovingDetailId(Guid.NewGuid()),
                 _movingId,
                 new InventoryItemId(addInventoryItemDto.Id),
                 new MovingDetailCount(addInventoryItemDto.Count)
             );
    }

    private async Task<WarehouseInventoryItem?> GetLastWarehouseInventoryItem(WarehouseId warehouseId, InventoryItemId inventoryItemId)
    {
        return await _warehouseInventoryItemRepository.Get(warehouseId, inventoryItemId);
    }
}
