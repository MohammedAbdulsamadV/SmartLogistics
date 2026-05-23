using Logistics.Domain.Enums.Order;
using MediatR;

namespace Logistics.Application.Features.Order.Admin.Commands;

public class UpdateOrderStatusCommand : IRequest<bool>
{
    public Guid OrderId { get; set; }
    public OrderStatus NewStatus { get; set; }

    public UpdateOrderStatusCommand(Guid OrderId, OrderStatus NewStatus)
    {
        this.OrderId = OrderId;
        this.NewStatus = NewStatus;
    }
}