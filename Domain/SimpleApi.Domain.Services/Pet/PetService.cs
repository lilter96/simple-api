using SimpleApi.Data.Persistent.Repositories;
using SimpleApi.Domain.Pet;
using SimpleApi.Domain.Pet.Dto;

namespace SimpleApi.Domain.Services.Pet;

public class PetService : IPetService
{
    private readonly IPetRepository _petRepository;

    public PetService(IPetRepository petRepository)
    {
        _petRepository = petRepository;
    }

    public async Task<BindPetToUserResponseDto> BindPetToUserAsync(Guid petId, Guid userId)
    {
        var canBeBound = await _petRepository.CheckPetCanBeBoundToUserAsync(petId, userId);

        if (!canBeBound)
        {
            return new BindPetToUserResponseDto
            {
                IsSuccess = false
            };
        }

        await _petRepository.BindPetToUserAsync(petId, userId);
        return new BindPetToUserResponseDto
        {
            IsSuccess = true
        };
    }

    public async Task<PetSummaryResponseDto> GetPetSummaryById(Guid petId)
    {
        var pet = await _petRepository.GetByIdAsync(petId);

        return new PetSummaryResponseDto
        {
            Id = pet.Id,
            Color = pet.Color,
            Name = pet.Name,
            UserId = pet.UserId,
            CollarId = pet.CollarId,
            Weight = pet.Weight
        };
    }

    public async Task<PetSummaryResponseDto> CreatePet(CreatePetRequestDto createPetRequestDto)
    {
        return await _petRepository.CreatePet(createPetRequestDto); ;
    }

    public Task<PetSummaryResponseDto> UpdatePet(UpdatePetRequestDto updatePetRequestDto)
    {
        return _petRepository.UpdatePet(updatePetRequestDto);
    }
}