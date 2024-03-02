using System;
using System.Collections.Generic;

namespace StorekeeperAssistant.UseCases.Movings.Commands.AddMoving.Dtos
{
    public record AddMovingDto(Guid? DepartureWarehouseId, Guid? ArrivalWarehouseId, List<AddInventoryItemDto> InventoryItems);
}
