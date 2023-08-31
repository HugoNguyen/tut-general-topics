using bookify.domain.Apartments;

namespace bookify.infrastructure.Repositories;
internal sealed class ApartmentRepository : Repository<Apartment>, IApartmentRepository
{
    public ApartmentRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
