using Logistics.Domain.Enums.Responsible;
using Logistics.Domain.ValueObjects.Integrations;
using MediatR;

namespace Logistics.Application.Features.Responsible;

public class AdminCreateResponsibleCommand: IRequest<Guid>
{
    public string Name { get; set; } = string.Empty;
    public ResponsibleType Type { get; set; }
    public string ContactPhone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? TaxNumber { get; set; }
    public ShippingIntegration? Integration { get; set; }
}