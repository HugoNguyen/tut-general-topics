using bookify.domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace bookify.infrastructure;
public sealed class ApplicationDbContext : DbContext, IUnitOfWork
{
    public ApplicationDbContext(DbContextOptions options)
        : base(options)
    {
    }
}
