using Logistics.Application.Interfaces;
using MediatR;

namespace Logistics.Application.Features.ShipmentLeg.Commands.Admin;

public class AdminAssignLegResponsibleCommandHandler : IRequestHandler<AdminAssignLegResponsibleCommand, bool>
{
    private readonly IShipmentRepository _shipmentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AdminAssignLegResponsibleCommandHandler(IShipmentRepository shipmentRepository, IUnitOfWork unitOfWork)
    {
        _shipmentRepository = shipmentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(AdminAssignLegResponsibleCommand request, CancellationToken cancellationToken)
    {
        var shipment = await _shipmentRepository.GetShipmentWithDetailsAsync(request.ShipmentId);
        if (shipment == null) throw new Exception("Shipment not found");

        var leg = shipment.ShipmentLegs.FirstOrDefault(x => x.Sequence == request.Sequence);
        if (leg == null) throw new Exception($"Shipment Leg {request.Sequence} not found");

        // بيزنس الأدمن: تعيين كابتن جديد للمحطة دي
        leg.AssignResponsible(request.NewResponsibleId);

        _shipmentRepository.Update(shipment);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}