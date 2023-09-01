using bookify.application.Abstractions.Messaging;

namespace bookify.application.Bookings.RejectBooking;
public record RejectBookingCommand(Guid BookingId) : ICommand;
