using BuildingBlocks.UseCases;
using Dapper;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using StorekeeperAssistant.UseCases.Warehouses.Queries.Dtos;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace StorekeeperAssistant.UseCases.Warehouses.Queries;

public record GetWarehousesQuery() : IRequest<IEnumerable<WarehouseDto>>;

public class GetWarehousesQueryHandler(ISqlConnectionFactory sqlConnectionFactory, IMemoryCache memoryCache) : IRequestHandler<GetWarehousesQuery, IEnumerable<WarehouseDto>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory = sqlConnectionFactory;
    private readonly IMemoryCache _memoryCache = memoryCache;

    public async Task<IEnumerable<WarehouseDto>> Handle(GetWarehousesQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<WarehouseDto>? result = [];

        if (_memoryCache.TryGetValue(nameof(GetWarehousesQuery), out result) == false)
        {
            var db = _sqlConnectionFactory.GetOpenConnection();

            result = await db.QueryAsync<WarehouseDto>(
                @"SELECT 
                    [Id], 
                    [Name] 
                FROM [Warehouses] 
                WHERE [IsDeleted] = @IsDeleted 
                ORDER BY [Name]",
                new
                {
                    IsDeleted = false
                });
        }

        return result!;
    }
}