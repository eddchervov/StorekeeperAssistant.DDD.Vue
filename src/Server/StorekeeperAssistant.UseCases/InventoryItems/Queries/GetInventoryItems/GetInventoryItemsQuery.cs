using BuildingBlocks.UseCases;
using Dapper;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace StorekeeperAssistant.UseCases.InventoryItems.Queries.GetInventoryItems;

public sealed record GetInventoryItemsQuery(): IRequest<IEnumerable<InventoryItemDto>>;

public sealed class GetInventoryItemsQueryHandler(ISqlConnectionFactory sqlConnectionFactory, IMemoryCache memoryCache) : IRequestHandler<GetInventoryItemsQuery, IEnumerable<InventoryItemDto>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory = sqlConnectionFactory;
    private readonly IMemoryCache _memoryCache = memoryCache;

    public async Task<IEnumerable<InventoryItemDto>> Handle(GetInventoryItemsQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<InventoryItemDto>? result = [];

        if (_memoryCache.TryGetValue(nameof(GetInventoryItemsQuery), out result) == false)
        {
            var db = _sqlConnectionFactory.GetOpenConnection();

            result = await db.QueryAsync<InventoryItemDto>(
                @"SELECT 
                    [Id],
                    [Name]
                FROM [InventoryItems]
                WHERE [IsDeleted] = @IsDeleted 
                ORDER BY [Name]",
                new
                {
                    IsDeleted = false
                });

            _memoryCache.Set(nameof(GetInventoryItemsQuery), result, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(60)));
        }

        return result!;
    }
}
