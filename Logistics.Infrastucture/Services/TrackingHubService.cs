using Logistics.API.Hubs;
using Logistics.Application.Common;
using Microsoft.AspNetCore.SignalR;

namespace Logistics.Infrastucture.Services;

public class TrackingHubService : ITrackingHubService
{
    private readonly IHubContext<TrackingHub> _hubContext;

    public TrackingHubService(IHubContext<TrackingHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task SendLocationUpdateAsync(Guid shipmentId, decimal latitude, decimal longitude)
    {
        await _hubContext.Clients.Group(shipmentId.ToString())
            .SendAsync("ReceiveLocationUpdate", new 
            {
                ShipmentId = shipmentId,
                Latitude = latitude,
                Longitude = longitude
            });
    }
}