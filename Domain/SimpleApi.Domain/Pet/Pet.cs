using System.Diagnostics;
using SimpleApi.Domain.Base;
using SimpleApi.Domain.Pet.Dto;

namespace SimpleApi.Domain.Pet;

public class Pet : Entity<Guid>, ICreated
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
    
    public string Weight { get; set; }
    
    public Guid? UserId { get; set; }

    public Guid? ZoneId { get; set; }
    
    public Guid? CollarId { get; set; }

    public DateTime Created { get; set; }

    public static Pet CreatePetForUser(CreatePetRequestDto createPetRequestDto)
    {
        var id = Guid.NewGuid();

        return new Pet(id)
        {
            Id = id,
            CollarId = createPetRequestDto.CollarId,
            Color = createPetRequestDto.Color,
            Created = DateTime.UtcNow,
            Name = createPetRequestDto.Name,
            UserId = createPetRequestDto.UserId,
            Weight = createPetRequestDto.Weight,
            ZoneId = createPetRequestDto.ZoneId
        };
    }
}