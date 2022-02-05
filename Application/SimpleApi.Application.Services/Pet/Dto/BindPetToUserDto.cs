namespace SimpleApi.Application.Services.Pet.Dto;

public class BindPetToUserDto
{
    public Guid PetId { get; set; }

    public Guid UserId { get; set; }
}