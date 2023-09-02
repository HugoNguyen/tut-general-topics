using bookify.application.Abstractions.Messaging;

namespace bookify.application.Bookings.CompleteBooking;
public record CompleteBookingCommand(Guid BookingId) : ICommand;