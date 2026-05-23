using Logistics.Application.DTOs;
using Logistics.Application.Interfaces;
using Logistics.Domain.Entities;
using MediatR;

namespace Logistics.Application.Features.Order.User.Queries.GetClientOrderDetailsQuery;

public class GetClientOrderDetailsQueryHandler : IRequestHandler<GetClientOrderDetailsQuery,GetOrderDetailsDto>
{
    private readonly IOrderRepository _orderRepository;

    public GetClientOrderDetailsQueryHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }
    public async Task<GetOrderDetailsDto> Handle(GetClientOrderDetailsQuery request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByTrackingNumberAsync(request.TrackingNumber,cancellationToken);
        if(order ==  null)
            throw new Exception("Order Not Found");
        return new GetOrderDetailsDto(order.TrackingNumber,new AddressDto(order.ShippingAddress.Street,
            order.ShippingAddress.State,
            order.ShippingAddress.City,
            order.ShippingAddress.ZipCode,
            order.ShippingAddress.Country),order.Status,order.OrderItems.ToList());
    }
}