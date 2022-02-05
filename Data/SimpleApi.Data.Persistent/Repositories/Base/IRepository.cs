using SimpleApi.Domain.Base;

namespace SimpleApi.Data.Persistent.Repositories.Base;

public interface IRepository<TEntity, in TId> where TEntity : IEntity<TId>
{
    public Task<TEntity> GetByIdAsync(TId id);

    public Task<IList<TEntity>> GetAllAsync();

    public Task<bool> DeleteByIdAsync(TId id);
}