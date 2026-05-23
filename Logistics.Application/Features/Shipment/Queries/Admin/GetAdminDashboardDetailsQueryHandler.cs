using Logistics.Application.DTOs;
using Logistics.Application.Interfaces;
using MediatR;

namespace Logistics.Application.Features.Shipment.Queries.Admin;

public class GetAdminDashboardDetailsQueryHandler : 
    IRequestHandler<GetAdminDashboardDetailsQuery, AdminShipmentDetailsDto>
{
    private readonly IShipmentRepository _shipmentRepository;

    public GetAdminDashboardDetailsQueryHandler(IShipmentRepository shipmentRepository)
    {
        _shipmentRepository = shipmentRepository;
    }

    public async Task<AdminShipmentDetailsDto> Handle(GetAdminDashboardDetailsQuery request, CancellationToken cancellationToken)
    {
        // سحب كامل للشحنة والـ Legs والـ Responsibles بتوعها من الـ Database
        var shipment = await _shipmentRepository.GetShipmentWithDetailsAsync(request.ShipmentId);
        if (shipment == null) throw new Exception("الشحنة غير موجودة لوحة التحكم.");

        var dto = new AdminShipmentDetailsDto
        {
            ShipmentId = shipment.Id,
            OrderId = shipment.OrderId,
            Status = shipment.Status.ToString(),
            VolumetricWeight = shipment.VolumetricWeight,
            RoutePolyline = shipment.RoutePolyline,
            CurrentLat = shipment.CurrentLocation?.Latitude,
            CurrentLng = shipment.CurrentLocation?.Longitude,
            DetailedLegs = new List<AdminLegDetailsDto>()
        };

        foreach (var leg in shipment.ShipmentLegs.OrderBy(x => x.Sequence))
        {
            dto.DetailedLegs.Add(new AdminLegDetailsDto
            {
                Sequence = leg.Sequence,
                TransportMode = leg.Mode.ToString(),
                FromHub = $"{leg.StartHub.Street}, {leg.StartHub.City}",
                ToHub = $"{leg.EndHub.Street}, {leg.EndHub.City}",
                LegStatus = leg.Status.ToString(),
                ResponsibleName = leg.Responsible?.Name ?? "لم يحدد بعد",
                ResponsibleType = leg.Responsible?.Type.ToString() ?? "N/A",
                ResponsiblePhone = leg.Responsible?.ContactPhone ?? "N/A",
                ActualStart = leg.ActualStart,
                ActualEnd = leg.ActualEnd
            });
        }

        return dto;
    }
}