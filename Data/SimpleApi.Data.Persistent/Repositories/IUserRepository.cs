using SimpleApi.Data.Persistent.Repositories.Base;
using SimpleApi.Domain.Pet;
using SimpleApi.Domain.User;
using SimpleApi.Domain.User.Dto;

namespace SimpleApi.Data.Persistent.Repositories;

public interface IUserRepository : IRepository<User, Guid>
{
    public Task<User> CreateAsync(CreateUserDto createUserDto);

    public Task<User> UpdateAsync(UpdateUserDto updateUserDto);

    public Task<IList<Pet>> GetUserPets(Guid id);
}