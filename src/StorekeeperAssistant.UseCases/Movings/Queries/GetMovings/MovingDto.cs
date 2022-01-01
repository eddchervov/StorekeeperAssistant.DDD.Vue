using System;
using System.Collections.Generic;

namespace StorekeeperAssistant.UseCases.Movings.Queries.GetMovings
{
    public class MovingDto
    {
        public Guid Id { get; set; }
        public List<MovingDetailDto> MovingDetails { get; set; } = new List<MovingDetailDto>();
        public GetMovingsWarehouseDto? DepartureWarehouse { get; set; }
        public GetMovingsWarehouseDto? ArrivalWarehouse { get; set; }
        public DateTime TransferDate { get; set; }
    }
}
