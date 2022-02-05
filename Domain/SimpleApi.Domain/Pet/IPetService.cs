using SimpleApi.Domain.Pet.Dto;

namespace SimpleApi.Domain.Pet;

public interface IPetService
{
    public Task<BindPetToUserResponseDto> BindPetToUserAsync(Guid petId, Guid userId);
}