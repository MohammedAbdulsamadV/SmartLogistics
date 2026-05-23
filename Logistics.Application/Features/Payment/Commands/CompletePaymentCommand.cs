using Logistics.Domain.Enums.Payment;
using MediatR;

namespace Logistics.Application.Features.Payment.Commands;

public class CompletePaymentCommand : IRequest<bool>
{
    public Guid OrderId { get; set; }
    public PaymentMethod Method { get; set; }
    public decimal Amount { get; set; }
}