using MediatR;

namespace Logistics.Domain.Common;

public interface IAggregateRoot{}


public abstract class BaseEntity 
{
    public Guid Id { get; set; } =  Guid.NewGuid();
    private readonly List<INotification> _domainEvents = new();
    public IReadOnlyCollection<INotification> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(INotification eventItem) => _domainEvents.Add(eventItem);
    public void ClearDomainEvents() => _domainEvents.Clear();
    
}