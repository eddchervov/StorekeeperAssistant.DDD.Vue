using System;
using System.Collections.Generic;

namespace StorekeeperAssistant.UseCases.Movings.Commands.AddMoving.Dtos;

public sealed record AddMovingDto(Guid? DepartureWarehouseId, Guid? ArrivalWarehouseId, List<AddInventoryItemDto> InventoryItems);
