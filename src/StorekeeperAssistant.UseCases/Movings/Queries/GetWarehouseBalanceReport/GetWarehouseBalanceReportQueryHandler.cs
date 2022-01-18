using BuildingBlocks.UseCases;
using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StorekeeperAssistant.UseCases.Movings.Queries.GetWarehouseBalanceReport
{
    public class GetWarehouseBalanceReportQueryHandler : IRequestHandler<GetWarehouseBalanceReportQuery, IEnumerable<WarehouseInventoryItemDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetWarehouseBalanceReportQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<IEnumerable<WarehouseInventoryItemDto>> Handle(GetWarehouseBalanceReportQuery request, CancellationToken cancellationToken)
        {
            var db = _sqlConnectionFactory.GetOpenConnection();

            var rows = await db.QueryAsync<dynamic>(
                " SELECT Id " +
                " INTO #TempInventoryItems " +
                " FROM dbo.InventoryItems " +
                " WHERE IsDeleted = 0 " +

                " CREATE TABLE #WarehouseBalanceReportRows " +
                " ([WarehouseInventoryItemId] uniqueidentifier, " +
                " [WarehouseInventoryItemDate] datetime2(7), " +
                " [WarehouseInventoryItemCount] int, " +
                " [InventoryItemId] uniqueidentifier, " +
                " [InventoryItemName] NVARCHAR(max), " +
                " [WarehouseId] uniqueidentifier) " +

                " DECLARE @InventoryItemID uniqueidentifier " +

                " WHILE exists(select * from #TempInventoryItems) " +
                " BEGIN " +

                " SELECT @InventoryItemID = (SELECT TOP 1 [Id] FROM #TempInventoryItems) " +

                " INSERT INTO #WarehouseBalanceReportRows " +
                " ([WarehouseInventoryItemId], " +
                " [WarehouseInventoryItemDate], " +
                " [WarehouseInventoryItemCount], " +
                " [InventoryItemId], " +
                " [InventoryItemName], " +
                " [WarehouseId]) " +

                " SELECT TOP (1)" +
                " wii.[Id], wii.[Date], wii.[Count], wii.[InventoryItemId], ii.[Name], wii.[WarehouseId] " +
                " FROM [dbo].[WarehouseInventoryItems] AS wii " +
                " LEFT JOIN [dbo].[InventoryItems] AS ii ON wii.[InventoryItemId] = ii.[Id] " +
                " WHERE wii.[WarehouseId] = @WarehouseId AND wii.[Date] <= @DateTime AND wii.[InventoryItemId] = @InventoryItemID " +
                " ORDER BY wii.[Date] DESC " +

                " DELETE #TempInventoryItems " +
                " WHERE [Id] = @InventoryItemID " +

                " END " +

                " DROP TABLE #TempInventoryItems " +

                " SELECT * FROM #WarehouseBalanceReportRows ",
                new
                {
                    request.WarehouseId,
                    request.DateTime
                });

            return MapToDto(rows);
        }

        private static IEnumerable<WarehouseInventoryItemDto> MapToDto(dynamic rows)
        {
            var warehouseInventoryItemDtos = new List<WarehouseInventoryItemDto>();

            foreach (var row in rows)
            {
                if (warehouseInventoryItemDtos.Any(x => x.InventoryItem.Id == row.InventoryItemId) == false && row.WarehouseInventoryItemCount > 0)
                    warehouseInventoryItemDtos.Add(new WarehouseInventoryItemDto
                    {
                        Id = row.WarehouseInventoryItemId,
                        Count = row.WarehouseInventoryItemCount,
                        InventoryItem = new InventoryItemDto
                        {
                            Id = row.InventoryItemId,
                            Name = row.InventoryItemName
                        }
                    });

            }

            return warehouseInventoryItemDtos.OrderBy(x => x.InventoryItem.Name);
        }
    }
}
