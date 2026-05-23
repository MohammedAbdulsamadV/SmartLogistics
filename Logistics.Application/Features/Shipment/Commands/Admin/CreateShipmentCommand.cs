using MediatR;

namespace Logistics.Application.Features.Shipment.Commands.Admin;

public class CreateShipmentCommand : IRequest<Guid>
{
    public Guid OrderId { get; set; }
    public decimal VolumetricWeight { get; set; }
}