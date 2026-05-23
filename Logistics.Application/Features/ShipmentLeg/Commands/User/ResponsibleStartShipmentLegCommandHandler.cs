using Logistics.Application.Interfaces;
using Logistics.Domain.Enums.Shipment;
using MediatR;

namespace Logistics.Application.Features.ShipmentLeg.Commands.User;

public class ResponsibleStartShipmentLegCommandHandler: IRequestHandler<ResponsibleStartShipmentLegCommand, bool>
{
    private readonly IShipmentRepository _shipmentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ResponsibleStartShipmentLegCommandHandler(IShipmentRepository shipmentRepository, IUnitOfWork unitOfWork)
    {
        _shipmentRepository = shipmentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(ResponsibleStartShipmentLegCommand request, CancellationToken cancellationToken)
    {
        var shipment = await _shipmentRepository.GetShipmentWithDetailsAsync(request.ShipmentId);
        if (shipment == null) throw new Exception("Shipment not found");

        var leg = shipment.ShipmentLegs.FirstOrDefault(x => x.Sequence == request.Sequence);
        if (leg == null) throw new Exception($"Shipment leg  {request.Sequence} not found");

        leg.StartLeg();

        if (request.Sequence == 1)
        {
            shipment.UpdateStatus(ShipmentStatus.InTransit);
        }

        _shipmentRepository.Update(shipment);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}