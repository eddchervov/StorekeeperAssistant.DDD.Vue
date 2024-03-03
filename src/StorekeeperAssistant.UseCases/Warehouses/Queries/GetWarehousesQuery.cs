using BuildingBlocks.UseCases;
using Dapper;
using MediatR;
using StorekeeperAssistant.UseCases.Warehouses.Queries.Dtos;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace StorekeeperAssistant.UseCases.Warehouses.Queries;

public record GetWarehousesQuery(): IRequest<IEnumerable<WarehouseDto>>;

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
            "WHERE [IsDeleted] = @IsDeleted " +
            "ORDER BY [Name]",
            new
            {
                IsDeleted = false
            });
    }
}