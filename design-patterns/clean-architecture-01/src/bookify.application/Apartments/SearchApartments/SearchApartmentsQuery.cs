using bookify.application.Abstractions.Messaging;

namespace bookify.application.Apartments.SearchApartments;
public sealed record SearchApartmentsQuery(
    DateOnly StartDate,
    DateOnly EndDate) : IQuery<IReadOnlyList<ApartmentResponse>>;
