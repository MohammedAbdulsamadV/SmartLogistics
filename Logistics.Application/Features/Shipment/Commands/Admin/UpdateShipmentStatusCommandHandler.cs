using Logistics.Application.Interfaces;
using MediatR;

namespace Logistics.Application.Features.Shipment.Commands.Admin;

public class UpdateShipmentStatusCommandHandler : IRequestHandler<UpdateShipmentStatusCommand,bool>
{
    private readonly IShipmentRepository _shipmentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateShipmentStatusCommandHandler(IShipmentRepository shipmentRepository, IUnitOfWork unitOfWork)
    {
        _shipmentRepository = shipmentRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<bool> Handle(UpdateShipmentStatusCommand request, CancellationToken cancellationToken)
    {
        var shipment = await _shipmentRepository.GetByIdAsync(request.ShipmentId);
        if (shipment == null) throw new Exception("Shipment not found");

        // نداء ميثود الـ Domain مباشرة
        shipment.UpdateStatus(request.NewStatus);

        _shipmentRepository.Update(shipment);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;    }
}