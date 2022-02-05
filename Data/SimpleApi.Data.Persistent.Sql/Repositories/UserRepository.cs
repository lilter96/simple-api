using Dapper;
using SimpleApi.Data.Persistent.Repositories;
using SimpleApi.Data.Persistent.Sql.Factories;
using SimpleApi.Domain.Pet;
using SimpleApi.Domain.User;
using SimpleApi.Domain.User.Dto;

namespace SimpleApi.Data.Persistent.Sql.Repositories;

public class UserRepository : BaseRepository<User, Guid>, IUserRepository
{
    public UserRepository(
        IDbConnectionFactory dbConnectionFactory) 
        : base(dbConnectionFactory)
    {
    }
    public async Task<User> CreateAsync(CreateUserDto createUserDto)
    {
        var sqlCreateUser = $"INSERT INTO {TableName} ({nameof(User.Name)}) OUTPUT INSERTED.* VALUES (@Name)";

        var user = await DbConnection.QueryFirstOrDefaultAsync<User>(sqlCreateUser, new {createUserDto.Name});

        if (user == null)
        {
            throw new InvalidDataException("Cannot insert a new user to the database.");
        }

        return user;
    }

    public async Task<User> UpdateAsync(UpdateUserDto updateUserDto)
    {
        var sqlUpdateUser = $"UPDATE {TableName} SET {nameof(User.Name)} = @Name OUTPUT INSERTED.* WHERE Id = @Id";

        var updatedUser = await DbConnection.QueryFirstOrDefaultAsync<User>(sqlUpdateUser, new
        {
            updateUserDto.Name,
            updateUserDto.Id
        });

        return updatedUser;
    }

    public async Task<IList<Pet>> GetUserPets(Guid id)
    {
        var secondTableName = nameof(Pet) + 's';
        var sqlGetUserPets =
            $"SELECT {secondTableName}.* " +
            $"FROM {TableName} " +
            $"JOIN {secondTableName} " +
            $"ON {TableName}.{nameof(User.Id)} = {secondTableName}.{nameof(Pet.UserId)} " +
            $"WHERE {TableName}.{nameof(User.Id)} = @Id";

        var pets = await DbConnection.QueryAsync<Pet>(sqlGetUserPets, new
        {
            Id = id
        });

        return pets.AsList();
    }
}