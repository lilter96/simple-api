using Dapper;
using SimpleApi.Data.Persistent.Exceptions;
using SimpleApi.Data.Persistent.Repositories;
using SimpleApi.Data.Persistent.Sql.Factories;
using SimpleApi.Domain.Pet;
using SimpleApi.Domain.Pet.Dto;

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

    public async Task<PetSummaryResponseDto> CreatePet(CreatePetRequestDto createPetRequestDto)
    {
        var pet = Pet.CreatePetForUser(createPetRequestDto);
        
        var sqlCreatePet =
            $"INSERT INTO {TableName} " +
            $"({nameof(Pet.Id)}, " +
            $"{nameof(Pet.Name)}, " +
            $"{nameof(Pet.Color)}, " +
            $"{nameof(Pet.Created)}, " +
            $"{nameof(Pet.Weight)}, " +
            $"{nameof(Pet.CollarId)}, " +
            $"{nameof(Pet.UserId)}, " +
            $"{nameof(Pet.ZoneId)}) " +
            "OUTPUT INSERTED.* " +
            "VALUES (" +
            $"@{nameof(Pet.Id)}, " +
            $"@{nameof(Pet.Name)}, " +
            $"@{nameof(Pet.Color)}, " +
            $"@{nameof(Pet.Created)}, " +
            $"@{nameof(Pet.Weight)}, " +
            $"@{nameof(Pet.CollarId)}, " +
            $"@{nameof(Pet.UserId)}, " +
            $"@{nameof(Pet.ZoneId)});";

        var createdPet = await DbConnection.QueryFirstOrDefaultAsync<Pet>(sqlCreatePet, new
        {
            pet.Id,
            pet.Name,
            pet.Color,
            pet.Created,
            pet.Weight,
            pet.CollarId,
            pet.UserId,
            pet.ZoneId
        });

        if (createdPet == null)
        {
            throw new NullReferenceException("Cannot create the pet.");
        }

        return new PetSummaryResponseDto
        {
            CollarId = createdPet.CollarId,
            Color = createdPet.Color,
            Id = createdPet.Id,
            Name = createdPet.Name,
            UserId = createdPet.UserId,
            Weight = createdPet.Weight
        };
    }

    public async Task<PetSummaryResponseDto> UpdatePet(UpdatePetRequestDto updatePetRequestDto)
    {
        var parameters = new DynamicParameters();

        var setPart = string.Empty;

        foreach (var property in updatePetRequestDto.GetType().GetProperties())
        {
            if (property.GetValue(updatePetRequestDto) == null)
            {
                continue;
            }

            parameters.Add($"@{property.Name}", property.GetValue(updatePetRequestDto));

            if (property.Name != nameof(Pet.Id))
            {
                setPart += $"{property.Name} = @{property.Name}, ";
            }
        }

        if (setPart.Length > 0)
        {
            setPart = setPart[..^2] + ' ';
        }

        var sqlUpdatePet =
            $"UPDATE {TableName} SET " +
            setPart +
            " OUTPUT INSERTED.* " +
            $"WHERE {nameof(Pet.Id)} = @{nameof(Pet.Id)}";

        var updatedPet = await DbConnection.QueryFirstOrDefaultAsync<Pet>(sqlUpdatePet, parameters);

        if (updatedPet == null)
        {
            throw new NullReferenceException("Cannot update the pet.");
        }

        return new PetSummaryResponseDto
        {
            CollarId = updatedPet.CollarId,
            Color = updatedPet.Color,
            Id = updatedPet.Id,
            Name = updatedPet.Name,
            UserId = updatedPet.UserId,
            Weight = updatedPet.Weight
        };
    }
}