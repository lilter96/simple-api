using AutoMapper;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using SimpleApi.Data.MongoDb.Factories;
using SimpleApi.Data.Persistent.Repositories.Base;
using SimpleApi.Domain.Base;

namespace SimpleApi.Data.MongoDb.Repositories;

public abstract class BaseRepository<TEntity, TId> : IRepository<TEntity, TId> where TEntity : Entity<TId>
{
    protected IMongoDatabase Database { get; }

    protected IMapper Mapper { get; }

    protected BaseRepository(
        IMongoDbClientFactory mongoDbClientFactory,
        IMapper mapper)
    {
        Database = mongoDbClientFactory.CreateMongoDbClient("MongoDb").GetDatabase(nameof(SimpleApi));
        Mapper = mapper;
    }

    public abstract Task<TEntity> GetByIdAsync(TId id);

    public abstract Task<IList<TEntity>> GetAllAsync();

    public abstract Task<bool> DeleteByIdAsync(TId id);
}