using Logistics.Application.Interfaces;
using Logistics.Domain.Exceptions.Order;
using MediatR;

namespace Logistics.Application.Features.Order.User.Commands.Cancel;

public class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand,bool>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;
    public CancelOrderCommandHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<bool> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
    {
        var  order = await _orderRepository.GetByTrackingNumberAsync(request.TrackingNumber, cancellationToken);
        if (order == null)
            throw new OrderDomainException($"Order with Tracking Number {request.TrackingNumber} not found");
        
        order.CancelOrder();
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}