using Logistics.Domain.Enums.Shipment;
using MediatR;

namespace Logistics.Application.Features.Shipment.Commands.Admin;

public class UpdateShipmentStatusCommand : IRequest<bool>
{
    public Guid ShipmentId { get; set; }
    public ShipmentStatus NewStatus { get; set; }
}