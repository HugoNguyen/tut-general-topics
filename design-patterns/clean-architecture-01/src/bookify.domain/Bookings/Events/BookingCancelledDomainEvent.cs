using bookify.domain.Abstractions;

namespace bookify.domain.Bookings.Events;
public sealed record BookingCancelledDomainEvent(Guid BookingId) : IDomainEvent;
