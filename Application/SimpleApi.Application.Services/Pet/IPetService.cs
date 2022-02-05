using SimpleApi.Application.Services.Pet.Dto;
using SimpleApi.Domain.Pet.Dto;

namespace SimpleApi.Application.Services.Pet;

public interface IPetService
{
    public Task<bool> BindPetToUserAsync(BindPetToUserDto bindPetToUserDto);

    public Task<PetSummaryResponseDto> CreatePet(CreatePetRequestDto createPetRequestDto);

    public Task<PetSummaryResponseDto> UpdatePet(UpdatePetRequestDto updatePetRequestDto);

    public Task<PetSummaryResponseDto> GetPetById(Guid id);
}