using Logistics.Application.Interfaces;
using MediatR;

namespace Logistics.Application.Features.Shipment.Commands.Admin;

public class CreateShipmentCommandHandler : IRequestHandler<CreateShipmentCommand, Guid>
{
    private readonly IShipmentRepository _shipmentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateShipmentCommandHandler(IShipmentRepository shipmentRepository, IUnitOfWork unitOfWork)
    {
        _shipmentRepository = shipmentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateShipmentCommand request, CancellationToken cancellationToken)
    {
        // استخدام الـ Constructor الحقيقي للـ Domain اللي بيخلي الحالة AtOrigin تلقائياً
        var shipment = new Domain.Entities.Shipment(request.OrderId);
        shipment.VolumetricWeight = request.VolumetricWeight;

        await _shipmentRepository.AddAsync(shipment);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return shipment.Id;
    }
}