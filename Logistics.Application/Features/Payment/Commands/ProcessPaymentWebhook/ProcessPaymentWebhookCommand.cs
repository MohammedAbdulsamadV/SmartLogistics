using MediatR;

namespace Logistics.Application.Features.Payment.Commands.ProcessPaymentWebhook;

public class ProcessPaymentWebhookCommand : IRequest<bool>
{
    public string TransactionReference { get; set; } = string.Empty;
    public string GatewayStatus { get; set; } = string.Empty;
}