namespace SimpleApi.Domain.Pet.Dto;

public class PetBasicInfoWithUsersIds : PetBasicInfo
{
    public Guid UserId { get; set; }
}