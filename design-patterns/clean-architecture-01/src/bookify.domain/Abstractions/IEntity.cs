namespace bookify.domain.Abstractions;

public interface IEntity
{
    void ClearDomainEvents();
    IReadOnlyList<IDomainEvent> GetDomainEvents();
}