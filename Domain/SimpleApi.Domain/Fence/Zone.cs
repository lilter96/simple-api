using SimpleApi.Domain.Base;
using SimpleApi.Domain.Fence.Geo;

namespace SimpleApi.Domain.Fence;

public class Zone : Entity<Guid>, ICreated
{
    public Zone(Guid id) 
        : base(id)
    {
    }

    // Required for Dapper
    protected Zone()
    {
    }

    public IList<GeoCoordinates> Coordinates { get; set; } = new List<GeoCoordinates>();

    public string Name { get; set; }

    public IList<Pet.Pet> Pets { get; set; } = new List<Pet.Pet>(); 

    public DateTime Created { get; set; }
}