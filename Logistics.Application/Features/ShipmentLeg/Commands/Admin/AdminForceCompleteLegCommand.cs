using MediatR;

namespace Logistics.Application.Features.ShipmentLeg.Commands.Admin;

public class AdminForceCompleteLegCommand: IRequest<bool>
{
    public Guid ShipmentId { get; set; }
    public int Sequence { get; set; }
    public string ReasonForOverride { get; set; } = string.Empty; 
}