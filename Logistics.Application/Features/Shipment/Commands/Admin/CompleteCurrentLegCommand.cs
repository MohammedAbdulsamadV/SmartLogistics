using MediatR;

namespace Logistics.Application.Features.Shipment.Commands.Admin;

public class CompleteCurrentLegCommand : IRequest<bool>
{
public Guid ShipmentId { get; set; }
}
