using Logistics.Domain.Common;
using Logistics.Domain.Enums;
using Logistics.Domain.Enums.Transportation;
using Logistics.Domain.ValueObjects.Order;

namespace Logistics.Domain.Entities;

public class ShipmentLeg : BaseEntity
{
    public int Sequence { get; private set; }
    public TransportMode Mode { get; private set; }
    public Address StartHub { get; private set; }
    public Address EndHub { get; private set; }
    public string? Notes { get; private set; }
    public Guid ResponsibleId { get; set; }
    public Responsible Responsible { get; set; }
    public LegStatus Status { get; private set; } = LegStatus.Pending;
    public DateTime? ActualStart { get; private set; }
    public DateTime? ActualEnd { get; private set; }
    public Guid ShipmentId { get; set; }
    public void StartLeg()
    {
        if (Status != LegStatus.Pending)
            throw new Exception($"You can not start now because status is {Status}");
        Status = LegStatus.Active;
        ActualStart = DateTime.UtcNow;
    }

    public void CompleteLeg()
    {
        if (Status != LegStatus.InProgress)
            throw new Exception($"You can not start now because status is {Status}");
        Status = LegStatus.Completed;
        ActualEnd = DateTime.UtcNow;
    }
    public void AssignResponsible(Guid responsibleId)
    {
        if (Status == LegStatus.Completed)
            throw new Exception("Shipment status is already completed");

        ResponsibleId = responsibleId;
    }
    
}