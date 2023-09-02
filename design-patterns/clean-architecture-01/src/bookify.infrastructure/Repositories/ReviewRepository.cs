using bookify.domain.Reviews;

namespace bookify.infrastructure.Repositories;
internal sealed class ReviewRepository : Repository<Review, ReviewId>, IReviewRepository
{
    public ReviewRepository(ApplicationDbContext dbContext)
        : base(dbContext)
    {
    }
}