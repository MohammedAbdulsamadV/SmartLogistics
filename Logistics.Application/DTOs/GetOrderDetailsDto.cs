using Logistics.Domain.Entities;
using Logistics.Domain.Enums.Order;

namespace Logistics.Application.DTOs;

public class GetOrderDetailsDto
{
    public string TrackingNumber { get; set; }
    public List<OrderItem> OrderItems { get; set; }
    public AddressDto Address { get; set; }
    public OrderStatus Status { get; set; }

    public GetOrderDetailsDto(string trackingNumber, AddressDto address, OrderStatus status , List<OrderItem> orderItems)
    {
        TrackingNumber = trackingNumber;
        Address = address;
        Status = status;
        OrderItems = orderItems;
    }
}