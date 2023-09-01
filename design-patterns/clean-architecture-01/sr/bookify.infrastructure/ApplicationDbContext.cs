using bookify.application.Exceptions;
using bookify.domain.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace bookify.infrastructure;
public sealed class ApplicationDbContext : DbContext, IUnitOfWork
{
    private readonly IPublisher _publisher;

    public ApplicationDbContext(DbContextOptions options, IPublisher publisher)
        : base(options)
    {
        _publisher = publisher;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Auto scan our entity configuations and apply them
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    /// <summary>
    /// After save all changes will publish events
    ///     There's a potential problem transtaction when changes saved successfully but the event handler got trouble
    /// => should resovlve with OUTBOX PATTERN
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await base.SaveChangesAsync(cancellationToken);

            await PublishDomainEventsAsync();

            return result;
        }
        catch(DbUpdateConcurrencyException ex)
        {
            throw new ConcurrencyException("Concurrency exception occurred", ex);
        }
    }

    private async Task PublishDomainEventsAsync()
    {
        var domainEvents = ChangeTracker
            .Entries<Entity>()
            .Select(entry => entry.Entity)
            .SelectMany(entity =>
            {
                var domainEvents = entity.GetDomainEvents();

                entity.ClearDomainEvents();

                return domainEvents;
            })
            .ToList();

        foreach(var domainEvent in domainEvents)
        {
            await _publisher.Publish(domainEvent);
        }
    }
}
