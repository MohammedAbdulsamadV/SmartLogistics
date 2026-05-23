using Logistics.Domain.Common;
using MediatR;

namespace Logistics.Domain.Events;

public record PaymentCompletedEvent(Guid OrderId, Guid PaymentId,decimal amount) : IDomainEvent
{
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
}