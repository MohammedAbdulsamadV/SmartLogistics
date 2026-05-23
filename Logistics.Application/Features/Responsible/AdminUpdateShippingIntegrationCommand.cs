using Logistics.Domain.Enums.Responsible;
using MediatR;

namespace Logistics.Application.Features.Responsible;

public class AdminUpdateShippingIntegrationCommand : IRequest<bool>
{
    public Guid ResponsibleId { get; set; }
    public IntegrationProvider Provider { get; set; }
    public string ApiKey { get; set; } = string.Empty;
    public string WebhookUrl { get; set; } = string.Empty;
    public string ApiBaseUrl { get; set; } = string.Empty;
}