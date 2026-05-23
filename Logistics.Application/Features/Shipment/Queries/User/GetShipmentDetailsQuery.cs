using Logistics.Application.DTOs;
using MediatR;

namespace Logistics.Application.Features.Shipment.Queries.User;

public class GetShipmentDetailsQuery : IRequest<ShipmentDetailsDto>
{
    public Guid ShipmentId { get; set; }
}