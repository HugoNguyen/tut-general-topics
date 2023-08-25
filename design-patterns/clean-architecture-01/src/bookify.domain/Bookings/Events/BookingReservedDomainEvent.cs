using bookify.domain.Abstractions;

namespace bookify.domain.Bookings.Events;
public sealed record BookingReservedDomainEvent(Guid BookingId) : IDomainEvent;
