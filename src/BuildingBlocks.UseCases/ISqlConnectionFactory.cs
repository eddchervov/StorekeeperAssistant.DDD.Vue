using System.Data;

namespace BuildingBlocks.UseCases;

public interface ISqlConnectionFactory
{
    IDbConnection GetOpenConnection();
}
