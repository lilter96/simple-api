namespace SimpleApi.Web.Api.Models.Pet;

public class BindPetToUserModel
{
    public Guid PetId { get; set; }
    
    public Guid UserId { get; set; }
}