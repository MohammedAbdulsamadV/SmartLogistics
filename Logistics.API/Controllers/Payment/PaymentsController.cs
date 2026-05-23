using Logistics.Application.Features.Payment.Commands.InitializePayment;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Logistics.API.Controllers.Payment;

public class PaymentsController : ApiControllerBase
{
    private readonly IMediator _mediator;

    public PaymentsController(IMediator mediator) => _mediator = mediator;

    [HttpPost("initialize")]
    public async Task<IActionResult> InitializePayment([FromBody] InitializePaymentCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result); // هيرجع الـ CheckoutUrl والـ Reference للفرونت إند علطول
    }
}