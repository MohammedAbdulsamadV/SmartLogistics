using Logistics.Application.Interfaces;
using MediatR;

namespace Logistics.Application.Features.Order.Admin.Queries.GetAllOrdersQuery;

public class GetAllOrdersHandler : IRequestHandler<GetAllOrders,IEnumerable<Domain.Entities.Order>>
{
    private readonly IOrderRepository _orderRepository;

    public GetAllOrdersHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }
    public async Task<IEnumerable<Domain.Entities.Order>> Handle(GetAllOrders request, CancellationToken cancellationToken)
    {
        return await  _orderRepository.GetAllAsync(cancellationToken); 
    }
}