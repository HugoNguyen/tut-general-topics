namespace bookify.domain.Abstractions;
public abstract class Entity<TEntityId> : IEntity
{
    /// <summary>
    /// Contain the domain events that raised on this entity
    /// </summary>
    private readonly List<IDomainEvent> _domainEvents = new();

    /// <summary>
    /// To statisfy Microsoft.EntityFrameworkCore.Tools
    /// </summary>
    protected Entity() { }

    protected Entity(TEntityId id)
    {
        Id = id;
    }

    public TEntityId Id { get; init; }

    public IReadOnlyList<IDomainEvent> GetDomainEvents()
    {
        return _domainEvents.ToList();
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    protected void RaiseDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}
