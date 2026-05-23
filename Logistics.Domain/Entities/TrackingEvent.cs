using Logistics.Domain.Common;
using Logistics.Domain.ValueObjects.Shipment;

namespace Logistics.Domain.Entities;

public class TrackingEvent : BaseEntity
{
    public Guid ShipmentId { get; private set; }
    public string LocationName { get; private set; }
    public string Description { get; private set; }
    public GpsCoordinates? Coordinates { get; private set; }
    public DateTime Timestamp { get; private set; } = DateTime.UtcNow;

    public TrackingEvent(Guid shipmentId, string location, string description, decimal? lat = null, decimal? lng = null)
    {
        ShipmentId = shipmentId;
        LocationName = location;
        Description = description;
        if (lat.HasValue && lng.HasValue)
            Coordinates = new GpsCoordinates(lat.Value, lng.Value);
    }
}