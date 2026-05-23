using Logistics.Application.DTOs;
using Logistics.Domain.Entities;
using Logistics.Domain.Enums.Order;
using MediatR;

namespace Logistics.Application.Features.Order.User.Commands.Cancel;

public class CancelOrderCommand : IRequest<bool>
{
    
    public OrderStatus Status { get; init; }
    public string TrackingNumber { get; set; } = string.Empty;
    public List<OrderItem> OrderItems { get; set; } = new();
    public AddressDto ShippingAddress { get; set; }
}