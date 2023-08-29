using bookify.domain.Abstractions;

namespace bookify.domain.Bookings.Events;

public sealed record BookingCompletedDomainEvent(Guid BookingId) : IDomainEvent;
