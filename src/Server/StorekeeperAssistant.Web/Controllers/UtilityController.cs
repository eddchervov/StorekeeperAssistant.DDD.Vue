using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StorekeeperAssistant.Domain.InventoryItems;
using StorekeeperAssistant.Domain.Warehouses;
using StorekeeperAssistant.UseCases.Interfaces;
using StorekeeperAssistant.UseCases.Movings.Commands.Common;
using StorekeeperAssistant.UseCases.Movings.Commands.CreateExpense;
using StorekeeperAssistant.UseCases.Movings.Commands.CreateIncome;
using StorekeeperAssistant.UseCases.Movings.Commands.CreateMoving;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StorekeeperAssistant.Web.Controllers;

[Route("api/utility")]
[ApiController]
public class UtilityController : ControllerBase
{
    private readonly ISender _sender;
    private readonly ILogger<UtilityController> _logger;
    private readonly IWarehouseRepository _warehouseRepository;
    private readonly IInventoryItemRepository _inventoryItemRepository;

    public UtilityController(
        ISender sender,
        ILogger<UtilityController> logger,
        IWarehouseRepository warehouseRepository,
        IInventoryItemRepository inventoryItemRepository)
    {
        _sender = sender;
        _logger = logger;
        _warehouseRepository = warehouseRepository;
        _inventoryItemRepository = inventoryItemRepository;
    }

    private static readonly int _maxCountOperations = 1000;

    [HttpPost("random-data-filling")]
    public async Task RandomDataFilling(int countOperations)
    {
        if (countOperations > _maxCountOperations) return;

        var random = new Random();

        var warehouses = await _warehouseRepository.GetAll();
        var inventoryItems = await _inventoryItemRepository.GetAll();

        for (int i = 0; i < countOperations; i++)
        {
            Warehouse warehouse = warehouses.Skip(random.Next(0, warehouses.Count())).First();

            var usedInventoryItems = new List<InventoryItem>();
            int countNewInventoryItems = random.Next(1, inventoryItems.Count() + 1);
            for (int j = 0; j < countNewInventoryItems; j++)
            {
                var usedInventoryItemIds = usedInventoryItems.Select(x => x.Id.Value);

                InventoryItem inventoryItem = inventoryItems
                    .Where(x => usedInventoryItemIds.Contains(x.Id.Value) == false)
                    .Skip(random.Next(0, inventoryItems.Count() - countNewInventoryItems))
                    .First();

                usedInventoryItems.Add(inventoryItem);
            }

            switch (random.Next(1, 5))
            {
                case 1:
                    try
                    {
                        await CreateExpense(warehouse, usedInventoryItems, random);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"RandomDataFilling - CreateExpense: {ex}");
                        goto case 2;
                    }
                    break;
    
                case 2:
                    try
                    {
                        await CreateIncome(warehouse, usedInventoryItems, random);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"RandomDataFilling - CreateIncome: {ex}");
                    }
                    break;
                case 3:
                case 4:
                case 5:
                default:
                    try
                    {
                        Warehouse arrivalWarehouse = warehouses.Where(x => x.Id != warehouse.Id).Skip(random.Next(1, warehouses.Count() - 1)).First();
                        await CreateMoving(warehouse, arrivalWarehouse, usedInventoryItems, random);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"RandomDataFilling - CreateMoving: {ex}");
                        goto case 2;
                    }
                    break;
            }
        }
    }

    private async Task CreateExpense(Warehouse warehouse, IEnumerable<InventoryItem> inventoryItems, Random random)
    {
        await _sender.Send(new CreateExpenseCommand
        {
            DepartureWarehouseId = warehouse.Id.Value,
            InventoryItems = inventoryItems.Select(x => new AddInventoryItemDto 
            {
                Id = x.Id.Value,
                Count = random.Next(1, 100)
            })
        });
    }

    private async Task CreateIncome(Warehouse warehouse, IEnumerable<InventoryItem> inventoryItems, Random random)
    {
        await _sender.Send(new CreateIncomeCommand
        {
            ArrivalWarehouseId = warehouse.Id.Value,
            InventoryItems = inventoryItems.Select(x => new AddInventoryItemDto
            {
                Id = x.Id.Value,
                Count = random.Next(1, 100)
            })
        });
    }

    private async Task CreateMoving(Warehouse departureWarehouse, Warehouse arrivalWarehouse, IEnumerable<InventoryItem> inventoryItems, Random random)
    {
        await _sender.Send(new CreateMovingCommand
        {
            DepartureWarehouseId = departureWarehouse.Id.Value,
            ArrivalWarehouseId = arrivalWarehouse.Id.Value,
            InventoryItems = inventoryItems.Select(x => new AddInventoryItemDto
            {
                Id = x.Id.Value,
                Count = random.Next(1, 100)
            })
        });
    }
}
