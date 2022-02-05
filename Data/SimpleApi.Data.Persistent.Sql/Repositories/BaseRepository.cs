using System.Data;
using System.Reflection;
using Dapper;
using SimpleApi.Data.Persistent.Exceptions;
using SimpleApi.Data.Persistent.Repositories.Base;
using SimpleApi.Data.Persistent.Sql.Factories;
using SimpleApi.Domain.Base;

namespace SimpleApi.Data.Persistent.Sql.Repositories;

public abstract class BaseRepository<TEntity, TId> : IRepository<TEntity, TId> where TEntity : IEntity<TId>
{
    private readonly IDbConnectionFactory _dbConnectionFactory;
    protected string TableName = typeof(TEntity).Name + "s";

    protected BaseRepository(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    protected IDbConnection DbConnection => _dbConnectionFactory.CreateDbConnection("Default");
    public async Task<TEntity> GetByIdAsync(TId id)
    {
        var sqlGetById = $"SELECT * FROM {TableName} WHERE Id = @Id";

        var entity = await DbConnection.QueryFirstOrDefaultAsync<TEntity>(sqlGetById, new { Id = id });

        if (entity == null)
        {
            throw new EntityNotFoundException($"Entity {nameof(TEntity)} with ID {id} was not found.");
        }

        return entity;
    }

    public async Task<IList<TEntity>> GetAllAsync()
    {
        var sqlGetAll = $"SELECT * FROM {TableName}";

        var entities = await DbConnection.QueryAsync<TEntity>(sqlGetAll);

        return entities.AsList();
    }

    public async Task<bool> DeleteByIdAsync(TId id)
    {
        var sqlDeleteById = $"DELETE FROM {TableName} WHERE Id = @Id";

        var result = await DbConnection.ExecuteAsync(sqlDeleteById, new { Id = id });

        return result > 0;
    }
}