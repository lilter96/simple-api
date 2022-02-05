using SimpleApi.Domain.Pet.Dto;

namespace SimpleApi.Domain.Pet;

public interface IPetService
{
    public Task<BindPetToUserResponseDto> BindPetToUserAsync(Guid petId, Guid userId);

    public Task<PetSummaryResponseDto> GetPetSummaryById(Guid petId);

    public Task<PetSummaryResponseDto> CreatePet(CreatePetRequestDto createPetRequestDto);

    public Task<PetSummaryResponseDto> UpdatePet(UpdatePetRequestDto updatePetRequestDto);
}