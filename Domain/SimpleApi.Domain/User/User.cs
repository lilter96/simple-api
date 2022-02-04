using SimpleApi.Domain.Base;

namespace SimpleApi.Domain.User;

public class User : Entity<Guid>
{
    public User(Guid id) : base(id)
    {
    }

    //Required for Dapper
    protected User()
    {
    }

    public string Name { get; set; }

    public IList<Pet.Pet> Pets { get; set; } = new List<Pet.Pet>();
}