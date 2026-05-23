using Microsoft.AspNetCore.SignalR;

namespace Logistics.API.Hubs;

public class TrackingHub : Hub
{
    public async Task SubscribeToShipment(string shipmentId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, shipmentId);
    }

    // الخروج من الـ Group عند قفل الخريطة
    public async Task UnsubscribeFromShipment(string shipmentId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, shipmentId);
    }
}