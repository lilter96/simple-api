using SimpleApi.Domain.Base;

namespace SimpleApi.Domain.Pet;

public class Pet : Entity<Guid>
{
    public Pet(Guid id) : base(id)
    {
    }

    //Required for Dapper
    protected Pet()
    {
    }

    public string Name { get; set; }

    public string Color { get; set; }
    
    public Guid UserId { get; set; }
}