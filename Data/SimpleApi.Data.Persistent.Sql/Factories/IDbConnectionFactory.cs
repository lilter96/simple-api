using System.Data;

namespace SimpleApi.Data.Persistent.Sql.Factories;

public interface IDbConnectionFactory
{
    public IDbConnection CreateDbConnection(string dbConnectionName);
}