using Logistics.Application.DTOs;
using MediatR;

namespace Logistics.Application.Features.Payment.Commands.InitializePayment;

public class InitializePaymentCommand : IRequest<PaymentInitResultDto>
{
    public Guid OrderId { get; set; }
    public int PaymentMethodNumber { get; set; } 
    public string PaymentProviderName { get; set; } = string.Empty; // "Stripe" أو "Paymob"
   
}