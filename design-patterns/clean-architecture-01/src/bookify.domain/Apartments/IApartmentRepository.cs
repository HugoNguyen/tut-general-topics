namespace bookify.domain.Apartments;
public interface IApartmentRepository
{
    Task<Apartment?> GetByIdAsync(Guid id, CancellationToken cancellation = default);
}
