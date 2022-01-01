using BuildingBlocks.UseCases;
using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StorekeeperAssistant.UseCases.Movings.Queries.GetMovings
{
    public class GetMovingsQueryHandler : IRequestHandler<GetMovingsQuery, GetMovingDto>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetMovingsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<GetMovingDto> Handle(GetMovingsQuery request, CancellationToken cancellationToken)
        {
            var db = _sqlConnectionFactory.GetOpenConnection();

            var multiple = await db.QueryMultipleAsync(
                "  SELECT COUNT(*) FROM [dbo].[Movings] " +
                "  SELECT " +
                "       m.[Id]                  AS MovindId, " +
                "       m.[TransferDate]        AS TransferDate, " +
                "       dw.[Id]                 AS DepartureWarehouseId, " +
                "       dw.[Name]               AS DepartureWarehouseName, " +
                "       aw.[Id]                 AS ArrivalWarehouseId, " +
                "       aw.[Name]               AS ArrivalWarehouseName, " +
                "       md.[Id]                 AS MovingDetailId, " +
                "       md.[InventoryItemId]    AS MovingDetailInventoryItemId, " +
                "       md.[Count]              AS MovingDetailCount, " +
                "       ii.[Name]               AS MovingDetailInventoryItemName " +
                "  FROM [dbo].[Movings] as m " +
                "    LEFT JOIN [dbo].[Warehouses]      AS dw ON m.DepartureWarehouseId = dw.Id " +
                "    LEFT JOIN [dbo].[Warehouses]      AS aw ON m.ArrivalWarehouseId = aw.Id " +
                "    LEFT JOIN [dbo].[MovingDetails]   AS md ON m.Id = md.MovingId " +
                "    LEFT JOIN [dbo].[InventoryItems]  AS ii ON md.InventoryItemId = ii.Id " +
                "  WHERE m.[IsDeleted] = @IsDeleted " +
                "  ORDER BY m.[TransferDate] DESC " +
                "  OFFSET @SkipCount ROWS " +
                "  FETCH NEXT @TakeCount ROWS ONLY",
                new
                {
                    IsDeleted = false,
                    request.SkipCount,
                    request.TakeCount
                });

            var totalCount = multiple.Read<int>().Single();
            var rows = multiple.Read<dynamic>();

            return new GetMovingDto { TotalCount = totalCount, Movings = MapToDto(rows) };
        }

        private static IEnumerable<MovingDto> MapToDto(dynamic rows)
        {
            var movingDtos = new List<MovingDto>();

            foreach (dynamic row in rows)
            {
                var movingDto = movingDtos.FirstOrDefault(x => x.Id == row.MovindId);
                if (movingDto == null) movingDto = CreateMovingDto(row);

                movingDto.MovingDetails.Add(CreateMovingDetailDto(row));

                movingDtos.Add(movingDto);
            }

            return movingDtos;
        }

        private static MovingDetailDto CreateMovingDetailDto(dynamic row)
        {
            return new MovingDetailDto
            {
                Id = row.MovingDetailId,
                Count = row.MovingDetailCount,
                InventoryItem = new InventoryItemDto
                {
                    Id = row.MovingDetailInventoryItemId,
                    Name = row.MovingDetailInventoryItemName
                }
            };
        }

        private static MovingDto CreateMovingDto(dynamic row)
        {
            return new MovingDto
            {
                Id = row.MovindId,
                TransferDate = row.TransferDate,
                ArrivalWarehouse = row.ArrivalWarehouseId != null ? new GetMovingsWarehouseDto
                {
                    Id = row.ArrivalWarehouseId,
                    Name = row.ArrivalWarehouseName
                } : null,
                DepartureWarehouse = row.DepartureWarehouseId != null ? new GetMovingsWarehouseDto
                {
                    Id = row.DepartureWarehouseId,
                    Name = row.DepartureWarehouseName
                } : null
            };
        }
    }
}
