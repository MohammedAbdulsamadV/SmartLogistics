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

        // 2. امسح الـ Events من الـ Entities عشان متتكررش
        domainEntities.ForEach(entity => entity.ClearDomainEvents());

        // 3. ابعت الـ Events للـ Handlers بتوعها في الـ Application
        foreach (var domainEvent in domainEvents)
            await _publisher.Publish(domainEvent, cancellationToken);

        // 4. احفظ في الداتابيز
        return await _Context.SaveChangesAsync(cancellationToken);
    }
}