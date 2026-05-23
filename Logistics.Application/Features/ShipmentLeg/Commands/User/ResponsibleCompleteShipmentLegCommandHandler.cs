using Logistics.Application.Interfaces;
using Logistics.Domain.Enums.Shipment;
using MediatR;

namespace Logistics.Application.Features.ShipmentLeg.Commands.User;

public class ResponsibleCompleteShipmentLegCommandHandler: IRequestHandler<ResponsibleCompleteShipmentLegCommand, bool>
{
    private readonly IShipmentRepository _shipmentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ResponsibleCompleteShipmentLegCommandHandler(IShipmentRepository shipmentRepository, IUnitOfWork unitOfWork)
    {
        _shipmentRepository = shipmentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(ResponsibleCompleteShipmentLegCommand request, CancellationToken cancellationToken)
    {
        var shipment = await _shipmentRepository.GetShipmentWithDetailsAsync(request.ShipmentId);
        if (shipment == null) throw new Exception("Shipment not found");

        var leg = shipment.ShipmentLegs.FirstOrDefault(x => x.Sequence == request.Sequence);
        if (leg == null) throw new Exception($"Shipment leg  {request.Sequence} not found");

        // بيزنس السائق: بيقفل الـ Leg بتاعته بنفسه
        leg.CompleteLeg();

        var maxSequence = shipment.ShipmentLegs.Max(x => x.Sequence);
        if (request.Sequence == maxSequence)
        {
            shipment.UpdateStatus(ShipmentStatus.Delivered);
        }

        _shipmentRepository.Update(shipment);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}