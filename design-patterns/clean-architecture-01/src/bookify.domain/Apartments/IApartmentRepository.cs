namespace bookify.domain.Apartments;
public interface IApartmentRepository
{
    Task<Apartment?> GetByIdAsync(ApartmentId id, CancellationToken cancellation = default);
}
