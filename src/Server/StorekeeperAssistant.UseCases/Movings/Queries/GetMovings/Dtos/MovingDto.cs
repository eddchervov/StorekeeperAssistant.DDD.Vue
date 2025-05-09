﻿using StorekeeperAssistant.Domain.Movings;
using System;
using System.Collections.Generic;

namespace StorekeeperAssistant.UseCases.Movings.Queries.GetMovings.Dtos;

public sealed class MovingDto
{
    public Guid Id { get; set; }
    public List<MovingDetailDto> MovingDetails { get; set; } = new List<MovingDetailDto>();
    public GetMovingsWarehouseDto? DepartureWarehouse { get; set; }
    public GetMovingsWarehouseDto? ArrivalWarehouse { get; set; }
    public DateTime TransferDate { get; set; }
    public MovementType MovementType { get; set; }
    public string MovementTypeText { get; set; } = default!;
}
