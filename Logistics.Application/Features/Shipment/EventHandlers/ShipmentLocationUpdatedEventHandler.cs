using Logistics.Application.Common;
using Logistics.Domain.Events;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Logistics.Application.Features.Shipment.EventHandlers;

public class ShipmentLocationUpdatedEventHandler : INotificationHandler<ShipmentLocationUpdatedEvent>
{
    private readonly ITrackingHubService _trackingHubService;

    public ShipmentLocationUpdatedEventHandler(ITrackingHubService trackingHubService)
    {
        _trackingHubService = trackingHubService;
    }

    public async Task Handle(ShipmentLocationUpdatedEvent notification, CancellationToken cancellationToken)
    {
        // إرسال الإحداثيات الجديدة فوراً لجميع الأجهزة (الموبايل أو الويب) اللي فاتحة خريطة التتبع دي بالذات
        await _trackingHubService.SendLocationUpdateAsync(
            notification.ShipmentId, 
            notification.Lat, 
            notification.Lng);
    }
}