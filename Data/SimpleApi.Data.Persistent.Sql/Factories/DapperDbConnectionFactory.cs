using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace SimpleApi.Data.Persistent.Sql.Factories;

public class DapperDbConnectionFactory : IDbConnectionFactory
{
    private readonly IConfiguration _configuration;

    public DapperDbConnectionFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IDbConnection CreateDbConnection(string dbConnectionName)
    {
        var connectionString = _configuration.GetConnectionString(dbConnectionName);

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new NullReferenceException($"The database connection string cannot be null.");
        }

        return new SqlConnection(connectionString);
    }
}