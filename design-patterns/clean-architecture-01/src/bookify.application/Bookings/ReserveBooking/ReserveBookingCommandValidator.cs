using FluentValidation;

namespace bookify.application.Bookings.ReserveBooking;

/// <summary>
/// Define a validator for ReserveBooingCommand
/// Will be trigger in validation behavior when the pipeline executed
/// </summary>
public class ReserveBookingCommandValidator : AbstractValidator<ReserveBookingCommand>
{
    public ReserveBookingCommandValidator()
    {
        RuleFor(c => c.UserId).NotEmpty();

        RuleFor(c => c.ApartmentId).NotEmpty();

        RuleFor(c => c.StartDate).LessThan(c => c.EndDate);
    }
}
