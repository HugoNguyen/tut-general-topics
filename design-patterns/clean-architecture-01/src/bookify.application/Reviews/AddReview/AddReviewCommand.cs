using bookify.application.Abstractions.Messaging;

namespace bookify.application.Reviews.AddReview;
public sealed record AddReviewCommand(Guid BookingId, int Rating, string Comment) : ICommand;
