using SimpleApi.Domain.Pet.Dto;

namespace SimpleApi.Domain.User.Dto;

public class UserBasicInfoWithPetsSummary
{
    public IList<PetBasicInfo> Pets { get; set; }
}