using System.ComponentModel.DataAnnotations;

namespace CFU.Domain.Seedwork;

public abstract class Entity
{
    public Entity()
    {
        Version = 0;
    }

    [ConcurrencyCheck]
    public long Version { get; protected set; }

    private List<DomainEvent> _domainEvents;
    public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents;

    public void IncrementVersion() => Version++;

    internal void AddDomainEvent(DomainEvent eventItem)
    {
        _domainEvents ??= new();
        _domainEvents.Add(eventItem);
    }

    internal void RemoveDomainEvent(DomainEvent eventItem)
    {
        _domainEvents?.Remove(eventItem);
    }

    public void ClearDomainEvents()
    {
        _domainEvents?.Clear();
    }
}

public abstract class Entity<TId> : Entity
{
    public TId Id { get; private set; }

    protected Entity(TId id)
    {
        Id = Guard.Against.Default(id, nameof(id));
    }

    public override bool Equals(object obj)
    {
        if (obj == null || obj is not Entity<TId>)
            return false;

        if (ReferenceEquals(this, obj))
            return true;

        if (GetType() != obj.GetType())
            return false;

        Entity<TId> item = (Entity<TId>)obj;

        return item.Id.Equals(Id);
    }

    public static bool operator ==(Entity<TId> left, Entity<TId> right)
    {
        if (Equals(left, null))
            return Equals(right, null);
        else
            return left.Equals(right);
    }

    public static bool operator !=(Entity<TId> left, Entity<TId> right) => !(left == right);

    public override int GetHashCode() => Id.GetHashCode();
}