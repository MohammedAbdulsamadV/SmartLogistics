using MediatR;

namespace Logistics.Application.Features.ShipmentLeg.Commands.User;

public class ResponsibleCompleteShipmentLegCommand
    : IRequest<bool>
{
    public Guid ShipmentId { get; set; }
    public int Sequence { get; set; }
}