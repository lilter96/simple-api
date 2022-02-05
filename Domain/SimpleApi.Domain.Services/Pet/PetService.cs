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
}