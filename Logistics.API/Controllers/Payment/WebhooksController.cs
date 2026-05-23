using System.Text.Json;
using Logistics.Application.Features.Payment.Commands.ProcessPaymentWebhook;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Logistics.API.Controllers.Payment;

public class WebhooksController : ApiControllerBase
{
    private readonly IMediator _mediator;

    public WebhooksController(IMediator mediator) => _mediator = mediator;
    [HttpPost("stripe")]
    public async Task<IActionResult> HandleStripeWebhook()
    {
        var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
        
        var command = new ProcessPaymentWebhookCommand
        {
            TransactionReference = "st_extracted_reference_from_json",
            GatewayStatus = "SUCCESS" 
        };

        await _mediator.Send(command);
        return Ok(); 
    }

    [HttpPost("paymob")]
    public async Task<IActionResult> HandlePaymobWebhook([FromQuery] string hmac, [FromBody] JsonElement payload)
    {
        
        var command = new ProcessPaymentWebhookCommand
        {
            TransactionReference = payload.GetProperty("obj").GetProperty("id").ToString(),
            GatewayStatus = payload.GetProperty("obj").GetProperty("success").GetBoolean() ? "SUCCESS" : "FAILED"
        };

        await _mediator.Send(command);
        return Ok();
    }
}