using Logistics.Application.DTOs;
using MediatR;

namespace Logistics.Application.Features.Shipment.Queries.Admin;

public class GetAdminDashboardDetailsQuery
    : IRequest<AdminShipmentDetailsDto>
{
    public Guid ShipmentId { get; set; }
}