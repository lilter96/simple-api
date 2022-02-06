using AutoMapper;
using ZoneMongoModel = SimpleApi.Data.MongoDb.Models.Zone;
using ZoneDomain = SimpleApi.Domain.Zone.Zone;

namespace SimpleApi.Data.MongoDb.Models.Profiles;

public class ZoneProfile : Profile
{
    public ZoneProfile()
    {
        CreateMap<ZoneMongoModel, ZoneDomain>();
    }
}