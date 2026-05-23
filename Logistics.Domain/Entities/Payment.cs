using Logistics.Domain.Common;
using Logistics.Domain.Enums.Payment;
using Logistics.Domain.Events;
using Logistics.Domain.ValueObjects.PaymentGateway;

namespace Logistics.Domain.Entities;

public class Payment : BaseEntity
{
    public Guid OrderId { get; private set; }
    public decimal Amount { get; private set; }
    public PaymentStatus Status { get; private set; }
    public PaymentMethod Method { get; private set; }
    public DateTime CreatedAt = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; private set; }
    public PaymentGatewayDetails? GatewayDetails { get; private set; }

    private Payment() { }

    public Payment(Guid orderId, decimal amount,PaymentMethod method)
    {
        OrderId = orderId;
        Amount = amount;
        Method = method;
        Status = PaymentStatus.Pending;
    }

    public void SetGatewayInfo(string provider, string reference, string url)
    {
        GatewayDetails = new PaymentGatewayDetails(provider, reference, url);
        UpdatedAt = DateTime.UtcNow;
    }

    public void Complete() 
    {
        if (Status == PaymentStatus.Completed) return;

        Status = PaymentStatus.Completed;
        UpdatedAt = DateTime.UtcNow;
        
        AddDomainEvent(new PaymentCompletedEvent(this.OrderId, this.Id, this.Amount));
    }
} 