using Logistics.Application.DTOs;
using MediatR;

namespace Logistics.Application.Features.Shipment.Queries.User;

public class GetClientTrackingQuery : IRequest<ClientTrackingDto>
{
    public Guid ShipmentId { get; set; }
}