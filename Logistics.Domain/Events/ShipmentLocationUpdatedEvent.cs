using Logistics.Domain.Common;
using MediatR;

namespace Logistics.Domain.Events;

public record ShipmentLocationUpdatedEvent(Guid ShipmentId, decimal Lat, decimal Lng) : DomainEvent
{
    
}