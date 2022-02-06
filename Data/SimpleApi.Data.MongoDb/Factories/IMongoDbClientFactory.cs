using MongoDB.Driver;

namespace SimpleApi.Data.MongoDb.Factories;

public interface IMongoDbClientFactory
{
    public IMongoClient CreateMongoDbClient(string connectionName);
}