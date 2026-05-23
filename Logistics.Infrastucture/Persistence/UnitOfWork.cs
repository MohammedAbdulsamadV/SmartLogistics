using Logistics.Application.Interfaces;
using Logistics.Domain.Common;
using MediatR;

namespace Logistics.Infrastucture.Persistence;

public class UnitOfWork : IUnitOfWork
{
    protected readonly ApplicationDbContext _Context;
    private readonly IPublisher _publisher;
    public UnitOfWork(ApplicationDbContext context,IPublisher publisher)
    {
        _Context = context;
        _publisher = publisher;
    }
    public void Dispose()
    {
        _Context.Dispose();
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var domainEntities = _Context.ChangeTracker
            .Entries<BaseEntity>()
            .Where(x => x.Entity.DomainEvents.Any())
            .Select(x => x.Entity)
            .ToList();

        var domainEvents = domainEntities.SelectMany(x => x.DomainEvents).ToList();

        domainEntities.ForEach(entity => entity.ClearDomainEvents());

        foreach (var domainEvent in domainEvents)
            await _publisher.Publish(domainEvent, cancellationToken);

        return await _Context.SaveChangesAsync(cancellationToken);
    }
}