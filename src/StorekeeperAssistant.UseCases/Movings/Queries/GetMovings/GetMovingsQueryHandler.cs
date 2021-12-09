using BuildingBlocks.UseCases;
using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace StorekeeperAssistant.UseCases.Movings.Queries.GetMovings
{
    public class GetMovingsQueryHandler : IRequestHandler<GetMovingsQuery, IEnumerable<MovingDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetMovingsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<IEnumerable<MovingDto>> Handle(GetMovingsQuery request, CancellationToken cancellationToken)
        {
            var db = _sqlConnectionFactory.GetOpenConnection();

            var result = await db.QueryAsync<dynamic>(
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

            return new List<MovingDto>();
        }
    }
}
