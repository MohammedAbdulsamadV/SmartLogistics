using Logistics.Application.Interfaces;
using MediatR;

namespace Logistics.Application.Features.Shipment.Commands.Admin;

public class UpdateLiveLocationCommandHandler : IRequestHandler<UpdateLiveLocationCommand, bool>
{
    private readonly IShipmentRepository _shipmentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateLiveLocationCommandHandler(IShipmentRepository shipmentRepository, IUnitOfWork unitOfWork)
    {
        _shipmentRepository = shipmentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(UpdateLiveLocationCommand request, CancellationToken cancellationToken)
    {
        var shipment = await _shipmentRepository.GetByIdAsync(request.ShipmentId);

        if (shipment == null)
            throw new Exception("Shipment not found!");

        shipment.UpdateLocation(request.Latitude, request.Longitude);

        _shipmentRepository.Update(shipment);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}