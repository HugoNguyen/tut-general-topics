using bookify.application.Abstractions.Messaging;

namespace bookify.application.Bookings.ReserveBooking;
public record ReserveBookingCommand(
    Guid ApartmentId,
    Guid UserId,
    DateOnly StartDate,
    DateOnly EndDate) : ICommand<Guid>;
