using Logistics.Domain.Common;
using Logistics.Domain.Enums;
using Logistics.Domain.Enums.Shipment;
using Logistics.Domain.Events;
using Logistics.Domain.ValueObjects.Shipment;

namespace Logistics.Domain.Entities;
//dashboard
public class Shipment : BaseEntity
{
    public decimal VolumetricWeight { get; set; }
    public ShipmentStatus Status { get; set; }
    public GpsCoordinates? CurrentLocation { get; private set; }
    public string? RoutePolyline { get; private set; }
    public Guid OrderId { get; private set; }
    
    private readonly List<ShipmentLeg> _legs = new();
    public  IReadOnlyCollection<ShipmentLeg> ShipmentLegs => _legs.AsReadOnly();
    private Shipment() { }

    public Shipment(Guid orderId)
    {
        OrderId = orderId;
        Status = ShipmentStatus.AtOrigin;
    }
    
    public void AddLeg(ShipmentLeg leg) => _legs.Add(leg);
    
    public void UpdateStatus(ShipmentStatus newStatus) => Status = newStatus;
    
    public void CompleteCurrentLegAndStartNext()
    {
        var currentLeg = _legs.OrderBy(x => x.Sequence)
            .FirstOrDefault(x => x.Status == LegStatus.Active);
        
        if (currentLeg != null)
        {
            currentLeg.CompleteLeg(); 
        }

        var nextLeg = _legs.OrderBy(x => x.Sequence)
            .FirstOrDefault(x => x.Status == LegStatus.Pending);
        
        if (nextLeg != null)
        {
            nextLeg.StartLeg(); 
        }
        else
        {
            this.Status = ShipmentStatus.Delivered;
        }
    }
    public void UpdateLocation(decimal lat, decimal lng)
    {
        this.CurrentLocation = new GpsCoordinates(lat, lng);
    
        
        AddDomainEvent(new ShipmentLocationUpdatedEvent(this.Id, lat, lng));
    }
}