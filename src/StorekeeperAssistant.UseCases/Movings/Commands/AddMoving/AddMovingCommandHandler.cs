using MediatR;
using StorekeeperAssistant.Domain.InventoryItemAggregate;
using StorekeeperAssistant.Domain.MovingAggregate;
using StorekeeperAssistant.Domain.MovingAggregate.MovingDetails;
using StorekeeperAssistant.Domain.MovingAggregate.WarehouseInventoryItems;
using StorekeeperAssistant.Domain.WarehouseAggregate;
using StorekeeperAssistant.UseCases.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StorekeeperAssistant.UseCases.Movings.Commands.AddMoving
{
    public class AddMovingCommandHandler : IRequestHandler<AddMovingCommand, Guid>
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
            var warehouseIds = request.GetWarehouseIds();
            var warehouses = await _warehouseRepository.GetByIds(warehouseIds);

            if (request.DepartureWarehouseId != null && warehouses.Any(x => x.Id.Value == request.DepartureWarehouseId) == false)
                throw new ArgumentException($"Склада отправления с id={request.DepartureWarehouseId.Value} не найдено");

            if (request.ArrivalWarehouseId != null && warehouses.Any(x => x.Id.Value == request.ArrivalWarehouseId) == false)
                throw new ArgumentException($"Склада прибытия с id={request.ArrivalWarehouseId.Value} не найдено");

            var inventoryItems = await _inventoryItemRepository.GetByIds(request.InventoryItems.Distinct().Select(x => new InventoryItemId(x.Id)));

            foreach (var inventoryItem in request.InventoryItems)
            {
                if (inventoryItems.Any(x => x.Id.Value == inventoryItem.Id) == false)
                    throw new ArgumentException($"Номенклатуры с id={inventoryItem.Id} не найдено");

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
                    request.DepartureWarehouseId != null ? new WarehouseId(request.DepartureWarehouseId.Value) : null,
                    request.ArrivalWarehouseId != null ? new WarehouseId(request.ArrivalWarehouseId.Value) : null
                ));

            await _movingRepository.SaveAsync();

            return _movingId.Value;
        }

        private Moving CreateMoving(WarehouseId? departureWarehouseId, WarehouseId? arrivalWarehouseId)
        {
            return new Moving(
                    _movingId,
                    _movingDetails,
                    _warehouseInventoryItems,
                    _utcNow,
                    departureWarehouseId,
                    arrivalWarehouseId
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
}
