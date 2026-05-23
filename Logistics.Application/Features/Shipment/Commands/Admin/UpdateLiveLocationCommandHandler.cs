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
        // 1. سحب الشحنة من الـ Repository برقم الـ Id
        var shipment = await _shipmentRepository.GetByIdAsync(request.ShipmentId);

        if (shipment == null)
            throw new Exception("الشحنة المطلوبة غير موجودة بالسيستم.");

        // 2. نداء ميثود الـ Domain الـ Encapsulated الحقيقية بتاعتك بالملي
        // الميثود دي بتعمل New لـ GpsCoordinates وترمي الـ ShipmentLocationUpdatedEvent جوه الـ Entity تلقائياً
        shipment.UpdateLocation(request.Latitude, request.Longitude);

        // 3. حفظ التعديلات في الداتابيز وتشغيل الـ Domain Events
        _shipmentRepository.Update(shipment);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}