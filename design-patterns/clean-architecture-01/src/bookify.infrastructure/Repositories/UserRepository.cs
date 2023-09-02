using bookify.domain.Users;

namespace bookify.infrastructure.Repositories;
internal sealed class UserRepository : Repository<User, UserId>, IUserRepository
{
    public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
