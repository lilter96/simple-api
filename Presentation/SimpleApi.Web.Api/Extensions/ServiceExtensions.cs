using System.Reflection;
using Dapper;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using SimpleApi.Domain.Base;
using SimpleApi.Domain.Pet;

namespace SimpleApi.Web.Api.Extensions;

public static class ServiceExtensions
{
    public static void AddBsonConventionsAndClassMaps(this IServiceCollection services)
    {
        BsonClassMap.RegisterClassMap<Entity<Guid>>(cm =>
        {
            cm.AutoMap();
            cm.MapIdMember(p => p.Id)
                .SetIdGenerator(new GuidGenerator())
                .SetSerializer(new GuidSerializer(BsonType.String));
            cm.SetIsRootClass(true);
        });

        var conventionPack = new ConventionPack { new CamelCaseElementNameConvention() };
        ConventionRegistry.Register("camelCase", conventionPack, _ => true);
    }
}