using System.Data;
using System.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Configuration;
using SimpleApi.Data.Persistent.Repositories;
using SimpleApi.Domain.Pet;
using SimpleApi.Domain.Pet.Dto;
using SimpleApi.Domain.User;
using SimpleApi.Domain.User.Dto;

namespace SimpleApi.Data.Persistent.Sql.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IDbConnection _dbConnection;
    private bool _isDbConnectionDisposed;

    public UserRepository(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Default");
        _dbConnection = new SqlConnection(connectionString);
    }

    ~UserRepository()
    {
        if (_isDbConnectionDisposed)
        {
            return;
        }

        _dbConnection.Dispose();
        _isDbConnectionDisposed = true;
    }

    public async Task<UserBasicInfo> GetAsync(Guid id)
    {
        const string sqlGetUser = "SELECT * FROM Users WHERE Id = @Id";

        var user = await _dbConnection.QueryFirstOrDefaultAsync<User>(sqlGetUser, new { Id = id });

        if (user == null)
        {
            return null;
        }

        return new UserBasicInfo
        {
            Id = user.Id,
            Name = user.Name
        };
    }

    public async Task<IList<UserBasicInfo>> GetAllAsync()
    {
        const string sqlGetAllUsers = "SELECT * FROM Users";

        var users = await _dbConnection.QueryAsync<User>(sqlGetAllUsers);

        return users.Select(user => new UserBasicInfo
        {
            Id = user.Id,
            Name = user.Name
        }).AsList();
    }

    public async Task<UserBasicInfo> CreateAsync(CreateUserDto createUserDto)
    {
        const string sqlCreateUser = "INSERT INTO Users (Name) OUTPUT INSERTED.* VALUES (@Name)";

        var user = await _dbConnection.QueryFirstOrDefaultAsync<User>(sqlCreateUser, new {createUserDto.Name});

        if (user == null)
        {
            return null;
        }

        return new UserBasicInfo
        {
            Id = user.Id,
            Name = user.Name
        };
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        const string sqlDeleteUserById = "DELETE FROM Users WHERE Id = @Id";

        var result = await _dbConnection.ExecuteAsync(sqlDeleteUserById, new {Id = id});

        return result == 1;
    }

    public async Task<UserBasicInfo> UpdateAsync(UpdateUserDto updateUserDto)
    {
        const string sqlUpdateUser = $"UPDATE Users SET {nameof(User.Name)} = @Name OUTPUT INSERTED.* WHERE Id = @Id";

        var result = await _dbConnection.QueryFirstOrDefaultAsync<User>(sqlUpdateUser, new
        {
            updateUserDto.Name,
            updateUserDto.Id
        });

        return new UserBasicInfo
        {
            Id = result.Id,
            Name = result.Name
        };
    }

    public async Task<UserBasicInfoWithPetsSummary> GetUserPets(Guid id)
    {
        const string sqlGetUserPets =
            "SELECT pets.* " +
            "FROM users " +
            "JOIN pets " +
            $"ON users.{nameof(User.Id)} = pets.{nameof(Pet.UserId)} " +
            $"WHERE users.{nameof(User.Id)} = @Id";

        var pets = await _dbConnection.QueryAsync<Pet>(sqlGetUserPets, new
        {
            Id = id
        });

        var petsSummary = pets.Select(pet => new PetBasicInfo
        {
            Id = pet.Id,
            Color = pet.Color,
            Name = pet.Name
        }).AsList();

        return new UserBasicInfoWithPetsSummary
        {
            Pets = petsSummary
        };
    }
}