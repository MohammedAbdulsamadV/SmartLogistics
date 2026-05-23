using Logistics.Application.DTOs;
using Logistics.Application.Interfaces;
using MediatR;

namespace Logistics.Application.Features.Shipment.Queries.User;

public class GetClientTrackingQueryHandler : IRequestHandler<GetClientTrackingQuery, ClientTrackingDto>
{
    private readonly IShipmentRepository _shipmentRepository;

    public GetClientTrackingQueryHandler(IShipmentRepository shipmentRepository)
    {
        _shipmentRepository = shipmentRepository;
    }

    public async Task<ClientTrackingDto> Handle(GetClientTrackingQuery request, CancellationToken cancellationToken)
    {
        var shipment = await _shipmentRepository.GetShipmentWithDetailsAsync(request.ShipmentId);
        if (shipment == null) throw new Exception("Shipment not found");

        var dto = new ClientTrackingDto
        {
            ShipmentId = shipment.Id,
            OverallStatus = shipment.Status.ToString(),
            CurrentLatitude = shipment.CurrentLocation?.Latitude,
            CurrentLongitude = shipment.CurrentLocation?.Longitude,
            TrackingTimeline = new List<ClientLegDto>()
        };

        foreach (var leg in shipment.ShipmentLegs.OrderBy(x => x.Sequence))
        {
            dto.TrackingTimeline.Add(new ClientLegDto
            {
                Sequence = leg.Sequence,
                Route = $"{leg.StartHub.City} -> {leg.EndHub.City}",
                Status = leg.Status.ToString(),
                ActualEnd = leg.ActualEnd
            });
        }

        return dto;
    }
}
