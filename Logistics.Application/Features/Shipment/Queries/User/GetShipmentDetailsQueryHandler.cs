using Logistics.Application.DTOs;
using Logistics.Application.Interfaces;
using MediatR;

namespace Logistics.Application.Features.Shipment.Queries.User;

public class GetShipmentDetailsQueryHandler : IRequestHandler<GetShipmentDetailsQuery,ShipmentDetailsDto>
{
    private readonly IShipmentRepository _shipmentRepository;

    public GetShipmentDetailsQueryHandler(IShipmentRepository shipmentRepository)
    {
        _shipmentRepository = shipmentRepository;
    }

    public async Task<ShipmentDetailsDto> Handle(GetShipmentDetailsQuery request, CancellationToken cancellationToken)
    {
        // سحب الشحنة بالـ Legs والـ Responsible بتوعها
        var shipment = await _shipmentRepository.GetShipmentWithDetailsAsync(request.ShipmentId);
        if (shipment == null) 
            throw new Exception("الشحنة المطلوبة غير موجودة.");

        var dto = new ShipmentDetailsDto
        {
            ShipmentId = shipment.Id,
            OverallStatus = shipment.Status.ToString(),
            VolumetricWeight = shipment.VolumetricWeight,
            CurrentLat = shipment.CurrentLocation?.Latitude,
            CurrentLng = shipment.CurrentLocation?.Longitude,
            Legs = new List<LegDto>()
        };

        // ترتيب الـ Legs بالـ Sequence عشان تطلع للأدمن أو اليوزر مظبوطة بالخطوات
        foreach (var leg in shipment.ShipmentLegs.OrderBy(x => x.Sequence))
        {
            dto.Legs.Add(new LegDto
            {
                Sequence = leg.Sequence,
                Mode = leg.Mode.ToString(),
                Status = leg.Status.ToString(),
                ResponsibleName = leg.Responsible?.Name ?? "لم يحدد بعد",
                ResponsibleType = leg.Responsible?.Type.ToString() ?? "N/A",
                ActualStart = leg.ActualStart,
                ActualEnd = leg.ActualEnd
            });
        }

        return dto;
    }
}