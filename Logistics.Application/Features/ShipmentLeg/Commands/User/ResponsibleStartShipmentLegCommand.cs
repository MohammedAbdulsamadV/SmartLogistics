using MediatR;

namespace Logistics.Application.Features.ShipmentLeg.Commands.User;

public class ResponsibleStartShipmentLegCommand: IRequest<bool>
{
    public Guid ShipmentId { get; set; }
    public int Sequence { get; set; }
}