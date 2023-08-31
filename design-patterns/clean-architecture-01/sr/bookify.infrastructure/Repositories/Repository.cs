using bookify.domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace bookify.infrastructure.Repositories;
internal abstract class Repository<T>
    where T : Entity
{
    protected readonly ApplicationDbContext DbContext;

    protected Repository(ApplicationDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellation = default)
    {
        return await DbContext
            .Set<T>()
            .FirstOrDefaultAsync(q => q.Id == id, cancellation);
    }

    public void Add(T entity)
    {
        DbContext.Add(entity);
    }
}
