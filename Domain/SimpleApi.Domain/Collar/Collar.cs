using SimpleApi.Domain.Base;

namespace SimpleApi.Domain.Collar;

public class Collar : Entity<Guid>, ICreated
{
    public Collar(Guid id) 
        : base(id)
    {
    }

    // Required for Dapper
    protected Collar()
    {
    }

    public string Name { get; set; }

    public string? Description { get; set; }

    public string Color { get; set; }

    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public int BatteryCharge { get; set; }

    public Guid? UserId { get; set; }

    public Guid? PetId { get; set; }

    public DateTime Created { get; set; }
}