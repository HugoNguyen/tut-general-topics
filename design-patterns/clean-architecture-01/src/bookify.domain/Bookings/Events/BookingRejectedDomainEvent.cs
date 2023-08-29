using bookify.domain.Abstractions;

namespace bookify.domain.Bookings.Events;

public sealed record BookingRejectedDomainEvent(Guid BookingId) : IDomainEvent;
