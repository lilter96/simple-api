using SimpleApi.Data.Persistent.Exceptions;
using SimpleApi.Data.Persistent.Repositories;
using SimpleApi.Domain.User;
using SimpleApi.Domain.User.Dto;

namespace SimpleApi.Domain.Services.User;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserWithPetsResponseDto> GetUserWithPets(Guid userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);

        if (user == null)
        {
            throw new EntityNotFoundException($"The user with ID {userId} was not found.");
        }

        var pets = await _userRepository.GetUserPets(userId);

        return new UserWithPetsResponseDto
        {
            Id = userId,
            Name = user.Name,
            Pets = pets
        };
    }
}