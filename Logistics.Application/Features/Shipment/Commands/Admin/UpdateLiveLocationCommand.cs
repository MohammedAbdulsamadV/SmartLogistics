using MediatR;

namespace Logistics.Application.Features.Shipment.Commands.Admin;

public class UpdateLiveLocationCommand : IRequest<bool>
{
    public Guid ShipmentId { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
}