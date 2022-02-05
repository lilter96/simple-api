using SimpleApi.Application.Services.Pet.Dto;
using IPetDomainService = SimpleApi.Domain.Pet.IPetService;

namespace SimpleApi.Application.Services.Pet;

public class PetService : IPetService
{
    private readonly IPetDomainService _petDomainService;

    public PetService(IPetDomainService petDomainService)
    {
        _petDomainService = petDomainService;
    }

    public async Task<bool> BindPetToUserAsync(BindPetToUserDto bindPetToUserDto)
    {
        var result = await _petDomainService.BindPetToUserAsync(bindPetToUserDto.PetId, bindPetToUserDto.UserId);

        return result.IsSuccess;
    }
}