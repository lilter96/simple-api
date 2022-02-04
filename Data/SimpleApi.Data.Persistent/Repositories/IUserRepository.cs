using SimpleApi.Domain.User.Dto;

namespace SimpleApi.Data.Persistent.Repositories;

public interface IUserRepository
{
    public Task<UserBasicInfo> GetAsync(Guid id);

    public Task<IList<UserBasicInfo>> GetAllAsync();

    public Task<UserBasicInfo> CreateAsync(CreateUserDto createUserDto);

    public Task<bool> DeleteAsync(Guid id);

    public Task<UserBasicInfo> UpdateAsync(UpdateUserDto updateUserDto);

    public Task<UserBasicInfoWithPetsSummary> GetUserPets(Guid id);
}