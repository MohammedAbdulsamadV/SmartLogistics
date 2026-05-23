using Logistics.Application.DTOs;
using MediatR;

namespace Logistics.Application.Features.Order.User.Queries.GetClientOrderDetailsQuery;

public class GetClientOrderDetailsQuery : IRequest<GetOrderDetailsDto>
{
    public string TrackingNumber { get; set; }
    public GetClientOrderDetailsQuery(string trackingNumber)
    {
        TrackingNumber = trackingNumber;
    }
}