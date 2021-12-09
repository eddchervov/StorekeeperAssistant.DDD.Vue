using BuildingBlocks.UseCases;
using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace StorekeeperAssistant.UseCases.Warehouses.Queries
{
    public class GetWarehousesQueryHandler : IRequestHandler<GetWarehousesQuery, IEnumerable<WarehouseDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetWarehousesQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<IEnumerable<WarehouseDto>> Handle(GetWarehousesQuery request, CancellationToken cancellationToken)
        {
            var db = _sqlConnectionFactory.GetOpenConnection();

            return await db.QueryAsync<WarehouseDto>(
                "SELECT " +
                "[Id], " +
                "[Name] " +
                "FROM [Warehouses] " +
                "WHERE [IsDeleted] = @isDeleted " +
                "ORDER BY [Name]",
                new
                {
                    isDeleted = false
                });
        }
    }
}
