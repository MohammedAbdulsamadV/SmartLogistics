using Logistics.Application.DTOs;
using Logistics.Domain.Entities;
using MediatR;

namespace Logistics.Application.Features.Order.User.Commands.Create;

public class CreateOrderCommand : IRequest<Guid>
{
    public string TrackingNumber { get; set; } = string.Empty;
    public string Email { get; set; } =  string.Empty;
    public List<OrderItem> OrderItems { get; set; }
    public AddressDto ShippingAddress { get; set; }

    public CreateOrderCommand()
    {
        OrderItems = new List<OrderItem>();
    }
} 