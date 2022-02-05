using Dapper;
using SimpleApi.Data.Persistent.Exceptions;
using SimpleApi.Data.Persistent.Repositories;
using SimpleApi.Data.Persistent.Sql.Factories;
using SimpleApi.Domain.Pet;

namespace SimpleApi.Data.Persistent.Sql.Repositories;

public class PetRepository : BaseRepository<Pet, Guid>, IPetRepository
{
    public PetRepository(IDbConnectionFactory dbConnectionFactory) 
        : base(dbConnectionFactory)
    {
    }

    public async Task<bool> CheckPetCanBeBoundToUserAsync(Guid petId, Guid userId)
    {
        var pet = await GetByIdAsync(petId);

        if (pet == null)
        {
            throw new EntityNotFoundException($"The pet with ID {petId} was not found.");
        }

        return pet.UserId == default;
    }

    public async Task BindPetToUserAsync(Guid petId, Guid userId)
    {
        var sqlBindPetToUser = $"UPDATE {TableName} SET {nameof(Pet.UserId)} = @UserId OUTPUT INSERTED.* WHERE Id = @PetId";

        var updatedPet = await DbConnection.QueryFirstOrDefaultAsync<Pet>(sqlBindPetToUser, new
        {
            UserId = userId,
            PetId = petId
        });

        if (updatedPet == null || updatedPet.UserId != userId)
        {
            throw new CannotBindPetToUserException("Something went wrong while binding pet to user.");
        }
    }
}