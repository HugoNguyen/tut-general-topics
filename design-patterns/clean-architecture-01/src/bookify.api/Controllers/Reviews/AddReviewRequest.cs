namespace bookify.api.Controllers.Reviews;

public sealed record AddReviewRequest(Guid BookingId, int Rating, string Comment);