namespace SimpleApi.Domain.Base;

public class Entity<T> : IEntity
{
    public T Id { get; protected set; }

    protected Entity(T id)
    {
        if (Equals(id, default(T)))
        {
            throw new ArgumentException("The ID cannot be the type's default value.", nameof(id));
        }

        Id = id;
    }

    // Required for Dapper
    protected Entity()
    {
    }

    public override bool Equals(object? obj)
    {
        if (obj is Entity<T> entity)
        {
            return Equals(entity);
        }

        return Equals(obj);
    }

    public virtual bool Equals(Entity<T>? other)
    {
        return other != null && Id!.Equals(other.Id);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}