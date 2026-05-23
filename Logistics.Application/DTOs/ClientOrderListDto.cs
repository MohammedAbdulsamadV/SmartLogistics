using Logistics.Domain.Entities;
using Logistics.Domain.Enums.Order;

namespace Logistics.Application.DTOs;

public class ClientOrderListDto
{
    public string TrackingNumber{ get; set; }
    public OrderStatus OrderStatus { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime DateTime { get; set; }
    public List<OrderItem> OrderItems { get; set; }

    public ClientOrderListDto(string trackingNumber, OrderStatus orderStatus, decimal totalAmount, List<OrderItem> orderItems)
    {
        TrackingNumber = trackingNumber;
        OrderStatus = orderStatus;
        TotalAmount = totalAmount;
        OrderItems = orderItems;
    }
}