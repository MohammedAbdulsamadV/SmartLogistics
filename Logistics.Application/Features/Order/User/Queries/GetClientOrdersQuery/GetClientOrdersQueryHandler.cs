using Logistics.Application.DTOs;
using Logistics.Application.Interfaces;
using MediatR;

namespace Logistics.Application.Features.Order.User.Queries.GetClientOrdersQuery;

public class GetClientOrdersQueryHandler : IRequestHandler<GetClientOrdersQuery,List<ClientOrderListDto>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICustomerRepository _customerRepository;
    public GetClientOrdersQueryHandler(IOrderRepository orderRepository, ICustomerRepository customerRepository)
    {
        _orderRepository = orderRepository;
        _customerRepository = customerRepository;
    }

    public async Task<List<ClientOrderListDto>> Handle(Queries.GetClientOrdersQuery.GetClientOrdersQuery request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.FindCustomerByEmailAsync(request.Email,cancellationToken);
        if (customer == null)
            throw new Exception("Customer not found");
        var orders = await _orderRepository.GetOrdersByCustomerIdAsync(customer.Id);
        return orders.Select(o => new ClientOrderListDto(
            o.TrackingNumber,o.Status,o.OrderItems.Sum(oi => oi.Quantity * oi.Price),o.OrderItems.ToList()
        )).ToList();
    }
}