using SimpleApi.Data.Persistent.Repositories.Base;
using SimpleApi.Domain.Pet;

namespace SimpleApi.Data.Persistent.Repositories;

public interface IPetRepository : IRepository<Pet, Guid>
{
    public Task<bool> CheckPetCanBeBoundToUserAsync(Guid petId, Guid userId);

    public Task BindPetToUserAsync(Guid petId, Guid userId);
}