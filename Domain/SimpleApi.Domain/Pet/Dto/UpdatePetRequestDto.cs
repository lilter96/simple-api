namespace SimpleApi.Domain.Pet.Dto;

public class UpdatePetRequestDto
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public string? Color { get; set; }

    public int? Weight { get; set; }
}