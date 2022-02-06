using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SimpleApi.Domain.Pet;
using SimpleApi.Domain.Zone.Geo;

namespace SimpleApi.Data.MongoDb.Models;

public class Zone
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }

    public IList<GeoCoordinates> Coordinates { get; set; }

    public Guid UserId { get; set; }

    public IList<Pet> Pets { get; set; } = new List<Pet>();

    public DateTime Created { get; set; }
}