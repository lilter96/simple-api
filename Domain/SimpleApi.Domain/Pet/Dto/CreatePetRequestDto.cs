using System.Text.Json.Serialization;

namespace SimpleApi.Domain.Pet.Dto;

public class CreatePetRequestDto : Pet
{
    [JsonIgnore]
    public new Guid Id { get; set; }
}