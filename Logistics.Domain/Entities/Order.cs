using Logistics.Domain.Common;
using Logistics.Domain.Enums.Order;
using Logistics.Domain.Exceptions.Order;
using Logistics.Domain.ValueObjects.Order;

namespace Logistics.Domain.Entities;

public class Order : BaseEntity , IAggregateRoot
{
    public string TrackingNumber { get; private set; }
    public Address ShippingAddress { get; private set; }
    public OrderStatus Status { get; private set; }
    public DateTime CreatedAt { get; protected set; } =  DateTime.UtcNow;
    public Guid CustomerId { get; private set; }
    public Customer Customer { get; set; }
    private readonly List<OrderItem> _orderItems = new List<OrderItem>();
    public  IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();
    
    
    private Order(){}
    
    public Order(string Email,string trackingNumber, Address shippingAddress)
    {
        if (string.IsNullOrWhiteSpace(trackingNumber))
            throw new ArgumentException("Tracking number is invalid");
        TrackingNumber = trackingNumber;
        ShippingAddress = shippingAddress;
        Status = OrderStatus.Pending;
        Email = Customer.Email;
        CustomerId = Customer.Id;
        
    }

    public void AddOrderItem(string productName, decimal price, int quantity)
    {
        var item = new OrderItem(productName, price, quantity);
        _orderItems.Add(item);
    }
    

    public void SetInTransit()
    {
        if (Status != OrderStatus.Confirmed)
            throw new OrderDomainException("Can't ship an order that is not confirmed");
        Status = OrderStatus.InTransit;
    }

    public void CancelOrder()
    {
        if (Status != OrderStatus.InTransit || Status != OrderStatus.Delivered)
            throw new OrderDomainException("Can't cancel an order already in Transit or Delivered");
        Status = OrderStatus.Cancelled;
    }

    public void UpdateOrderDetails(Address newAddress, OrderStatus newStatus, string trackingNumber,List<OrderItem> newItems)
    {
        if(this.Status == OrderStatus.Cancelled)
            throw new OrderDomainException("Can't cancel an order that is already cancelled");
        if (this.Status == OrderStatus.Delivered)
            throw new OrderDomainException("Can't cancel an order that is delivered");
        if (this.Status == OrderStatus.InTransit)
            throw new OrderDomainException("Can't cancel an order that is in transit");
        this.TrackingNumber = trackingNumber;
        this.ShippingAddress = newAddress;
        this.Status = newStatus;
        foreach (var item in newItems)
        {
            AddOrderItem(item.ProductName, item.Price, item.Quantity);
        }
    }
    public bool CanBePaid() => Status == OrderStatus.Pending;

    public void UpdateOrderStatus(OrderStatus newStatus)
    {
        Status = newStatus;
    }

    public decimal GetTotalPrice()
    {
        decimal totalPrice = 0;
        foreach (var item in _orderItems)
        {
            totalPrice += item.Price * item.Quantity;
        }
        return totalPrice;
    }
}