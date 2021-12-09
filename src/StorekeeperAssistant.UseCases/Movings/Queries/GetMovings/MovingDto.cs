using System;
using System.Collections.Generic;

namespace StorekeeperAssistant.UseCases.Movings.Queries.GetMovings
{
    public class MovingDto
    {
        public Guid Id { get; set; }
        public IEnumerable<MovingDetailDto> MovingDetails { get; set; } = new List<MovingDetailDto>();
        public WarehouseDto DepartureWarehouse { get; set; } = default!;
        public WarehouseDto ArrivalWarehouse { get; set; } = default!;
        public DateTime TransferDate { get; set; }
    }
}
