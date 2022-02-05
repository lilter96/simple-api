namespace SimpleApi.Web.Api.Models.Pet;

public class UpdatePetModel
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }

    public string Color { get; set; }
}