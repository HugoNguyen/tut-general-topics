using bookify.application.Abstractions.Messaging;

namespace bookify.application.Bookings.GetBooking;
public sealed record GetBookingQuery(Guid BookingId) : IQuery<BookingResponse>
{
}
