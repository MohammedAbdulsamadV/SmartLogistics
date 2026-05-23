using AutoMapper;
using Logistics.Application.DTOs;
using Logistics.Domain.Entities;


namespace Logistics.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Order,OrderDto>().ForMember(dest => dest.ShippingAddress,
            opt => 
                opt.MapFrom(src => $"{src.ShippingAddress.Street}, " +
                                         $"{src.ShippingAddress.City}, " +
                                         $"{src.ShippingAddress.Country}"))
            .ForMember(dest => dest.TotalAmount,
                opt => opt.MapFrom
                    (src => src.OrderItems.Sum
                        (orderItem => orderItem.Price * orderItem.Quantity)));
        CreateMap<OrderItem,OrderItemResponse>().ForMember(dest => dest.TotalItemPrice,opt =>
                opt.MapFrom(src => src.Quantity * src.Price));
        CreateMap<Shipment, ShipmentResponse>()
            .ForMember(dest => dest.Latitude, opt => opt.MapFrom(src => src.CurrentLocation.Latitude))
            .ForMember(dest => dest.Longitude, opt => opt.MapFrom(src => src.CurrentLocation.Longitude));
        CreateMap<ShipmentLeg, LegResponse>();
        
    }
}

