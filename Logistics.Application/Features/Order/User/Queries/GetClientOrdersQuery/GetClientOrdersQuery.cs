using Logistics.Application.DTOs;
using MediatR;

namespace Logistics.Application.Features.Order.User.Queries.GetClientOrdersQuery;

public class GetClientOrdersQuery : IRequest<List<ClientOrderListDto>>
{
    public string Email { get; set; }
    
}