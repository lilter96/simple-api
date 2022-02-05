namespace SimpleApi.Domain.User.Dto;

public class UserWithPetsResponseDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public IList<Pet.Pet> Pets { get; set; } = new List<Pet.Pet>();
}