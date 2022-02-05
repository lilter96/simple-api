using SimpleApi.Application.Services.Pet.Dto;

namespace SimpleApi.Application.Services.Pet;

public interface IPetService
{
    public Task<bool> BindPetToUserAsync(BindPetToUserDto bindPetToUserDto);
}