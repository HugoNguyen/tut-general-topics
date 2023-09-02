using bookify.application.Abstractions.Messaging;

namespace bookify.application.Bookings.ConfirmBooking;
public record ConfirmBookingCommand(Guid BookingId) : ICommand;
