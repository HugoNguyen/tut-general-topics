using bookify.domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace bookify.infrastructure.Repositories;
internal abstract class Repository<TEntity, TEntityId>
    where TEntity : Entity<TEntityId>
    where TEntityId : class
{
    protected readonly ApplicationDbContext DbContext;

    protected Repository(ApplicationDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public async Task<TEntity?> GetByIdAsync(TEntityId id, CancellationToken cancellation = default)
    {
        return await DbContext
            .Set<TEntity>()
            .FirstOrDefaultAsync(q => q.Id == id, cancellation);
    }

    public void Add(TEntity entity)
    {
        DbContext.Add(entity);
    }
}
