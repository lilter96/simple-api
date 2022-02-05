using SimpleApi.Domain.User.Dto;

namespace SimpleApi.Domain.User;

public interface IUserService
{
    public Task<UserWithPetsResponseDto> GetUserWithPets(Guid userId);
}