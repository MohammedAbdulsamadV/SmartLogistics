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
        var shipment = await _shipmentRepository.GetShipmentWithDetailsAsync(request.ShipmentId);
        if (shipment == null) 
            throw new Exception("Shipment not found!");

        var dto = new ShipmentDetailsDto
        {
            ShipmentId = shipment.Id,
            OverallStatus = shipment.Status.ToString(),
            VolumetricWeight = shipment.VolumetricWeight,
            CurrentLat = shipment.CurrentLocation?.Latitude,
            CurrentLng = shipment.CurrentLocation?.Longitude,
            Legs = new List<LegDto>()
        };

        foreach (var leg in shipment.ShipmentLegs.OrderBy(x => x.Sequence))
        {
            dto.Legs.Add(new LegDto
            {
                Sequence = leg.Sequence,
                Mode = leg.Mode.ToString(),
                Status = leg.Status.ToString(),
                ResponsibleName = leg.Responsible?.Name ?? "Not Selected",
                ResponsibleType = leg.Responsible?.Type.ToString() ?? "N/A",
                ActualStart = leg.ActualStart,
                ActualEnd = leg.ActualEnd
            });
        }

        return dto;
    }
}