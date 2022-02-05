namespace SimpleApi.Domain.Pet.Dto;

public class PetSummaryResponseDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Color { get; set; }

    public string Weight { get; set; }

    public Guid? UserId { get; set; }

    public Guid? CollarId { get; set; }
}