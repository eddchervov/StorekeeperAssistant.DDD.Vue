using System;
using System.Collections.Generic;

namespace StorekeeperAssistant.UseCases.Movings.Commands.AddMoving
{
    public record AddMovingDto(Guid? DepartureWarehouseId, Guid? ArrivalWarehouseId, List<AddInventoryItemDto> InventoryItems);
}
