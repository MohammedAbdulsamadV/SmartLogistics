using Logistics.Domain.Entities;
using Logistics.Domain.Enums.Order;

namespace Logistics.Application.DTOs;

public class OrderDto
{
    public string TrackingNumber { get; set; } = string.Empty;
    public List<OrderItem> OrderItems { get; set; } = new();
    public AddressDto ShippingAddress { get; set; }
    public OrderStatus Status { get; set; }
    public decimal TotalAmount { get; set; }
}