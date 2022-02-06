using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace SimpleApi.Data.MongoDb.Factories;

public class MongoDbClientFactory : IMongoDbClientFactory
{
    private readonly IConfiguration _configuration;

    public MongoDbClientFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IMongoClient CreateMongoDbClient(string connectionName)
    {
        var connectionString = _configuration.GetConnectionString(connectionName);

        return new MongoClient(connectionString);
    }
}