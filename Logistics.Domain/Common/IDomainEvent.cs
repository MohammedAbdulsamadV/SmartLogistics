using MediatR;

namespace Logistics.Domain.Common;

public interface IDomainEvent : INotification
{
    DateTime OccurredOn { get; }
}