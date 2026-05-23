using Logistics.Application.Interfaces;
using MediatR;

namespace Logistics.Application.Features.Shipment.Commands.Admin;

public class CompleteCurrentLegCommandHandler : IRequestHandler<CompleteCurrentLegCommand, bool>
{
    private readonly IShipmentRepository _shipmentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CompleteCurrentLegCommandHandler(IShipmentRepository shipmentRepository, IUnitOfWork unitOfWork)
    {
        _shipmentRepository = shipmentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(CompleteCurrentLegCommand request, CancellationToken cancellationToken)
    {
        var shipment = await _shipmentRepository.GetShipmentWithDetailsAsync(request.ShipmentId);
        if (shipment == null) throw new Exception("Shipment not found");

        shipment.CompleteCurrentLegAndStartNext();

        _shipmentRepository.Update(shipment);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}
