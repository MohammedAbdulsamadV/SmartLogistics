using Logistics.Application.Interfaces;
using Logistics.Domain.ValueObjects.Order;
using MediatR;

namespace Logistics.Application.Features.Order.User.Commands.Create;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand,Guid>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;
    public CreateOrderCommandHandler(ICustomerRepository customerRepository,IOrderRepository orderRepository, IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
        _customerRepository = customerRepository;
    }

    public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var Customer =  _customerRepository.FindCustomerByEmailAsync(request.Email,cancellationToken);
        if (Customer == null)
        {
            throw new Exception("Customer not found");
        }
        var shippingAddress = new Address(
            request.ShippingAddress.Street, request.ShippingAddress.City,request.ShippingAddress.State, request.ShippingAddress.ZipCode,
            request.ShippingAddress.Country);
        var order = new Domain.Entities.Order(request.Email,request.TrackingNumber, shippingAddress);
        if (request.OrderItems != null && request.OrderItems.Any())
        {
            foreach (var orderItem in request.OrderItems)
            {
                order.AddOrderItem(orderItem.ProductName, orderItem.Price, orderItem.Quantity);
            }
        }
        await _orderRepository.AddAsync(order);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return order.Id;
    }
}