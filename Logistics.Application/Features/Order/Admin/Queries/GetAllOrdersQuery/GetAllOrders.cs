using MediatR;

namespace Logistics.Application.Features.Order.Admin.Queries.GetAllOrdersQuery;

public class GetAllOrders : IRequest<IEnumerable<Domain.Entities.Order>>
{
    public GetAllOrders()
    {
        
    }
}