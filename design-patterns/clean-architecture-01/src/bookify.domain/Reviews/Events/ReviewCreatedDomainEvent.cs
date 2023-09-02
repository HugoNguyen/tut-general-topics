using bookify.domain.Abstractions;

namespace bookify.domain.Reviews.Events;
public sealed record ReviewCreatedDomainEvent(ReviewId ReviewId) : IDomainEvent;