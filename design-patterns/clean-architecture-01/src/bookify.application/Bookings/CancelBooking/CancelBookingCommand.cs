using bookify.application.Abstractions.Messaging;

namespace bookify.application.Bookings.CancelBooking;
public record CancelBookingCommand(Guid BookingId) : ICommand;
