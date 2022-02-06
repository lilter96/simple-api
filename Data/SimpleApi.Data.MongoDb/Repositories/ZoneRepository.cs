using AutoMapper;
using MongoDB.Bson;
using MongoDB.Driver;
using SimpleApi.Data.MongoDb.Factories;
using SimpleApi.Data.Persistent.Exceptions;
using Zone = SimpleApi.Domain.Zone.Zone;


namespace SimpleApi.Data.MongoDb.Repositories;

public class ZoneRepository : BaseRepository<Zone, Guid>
{
    private readonly IMongoCollection<Zone> _zoneCollection;

    public ZoneRepository(
        IMongoDbClientFactory mongoDbClientFactory,
        IMapper mapper) 
        : base(mongoDbClientFactory, mapper)
    {
        _zoneCollection = Database.GetCollection<Zone>(nameof(Zone) + 's');
    }

    public override async Task<Zone> GetByIdAsync(Guid id)
    {
        var filter = Builders<Zone>.Filter.Eq(entity => entity.Id, id);

        var zone = await _zoneCollection.Find(filter).FirstOrDefaultAsync();

        if (zone == null)
        {
            throw new EntityNotFoundException($"The zone with Id {id} was not found in the db.");
        }
        
        return zone;
    }

    public override async Task<IList<Zone>> GetAllAsync()
    {
        var zones = (await _zoneCollection.FindAsync(new BsonDocument())).ToList();

        if (zones == null)
        {
            throw new NullReferenceException("Something went wrong while getting all zones from the db.");
        }

        return zones;
    }

    public override async Task<bool> DeleteByIdAsync(Guid id)
    {
        var filter = Builders<Zone>.Filter.Eq(entity => entity.Id, id);

        var result = await _zoneCollection.DeleteOneAsync(filter);

        return result.DeletedCount > 0;
    }
}