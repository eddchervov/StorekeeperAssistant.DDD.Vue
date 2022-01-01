using BuildingBlocks.UseCases;
using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace StorekeeperAssistant.UseCases.InventoryItems.Queries.GetInventoryItems
{
    public class GetInventoryItemsQueryHandler : IRequestHandler<GetInventoryItemsQuery, IEnumerable<InventoryItemDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetInventoryItemsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<IEnumerable<InventoryItemDto>> Handle(GetInventoryItemsQuery request, CancellationToken cancellationToken)
        {
            var db = _sqlConnectionFactory.GetOpenConnection();

            return await db.QueryAsync<InventoryItemDto>(
                "SELECT " +
                "[Id], " +
                "[Name] " +
                "FROM [InventoryItems] " +
                "WHERE [IsDeleted] = @IsDeleted " +
                "ORDER BY [Name]",
                new
                {
                    IsDeleted = false
                });
        }
    }
}
