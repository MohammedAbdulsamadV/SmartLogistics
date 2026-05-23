using MediatR;

namespace Logistics.Application.Features.ShipmentLeg.Commands.Admin;

public class AdminAssignLegResponsibleCommand: IRequest<bool>
{
    public Guid ShipmentId { get; set; }
    public int Sequence { get; set; }
    public Guid NewResponsibleId { get; set; }
}