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
        // بنقرأ الـ JSON اللي جاي من Stripe ونطلع منه رقم الـ Reference والـ Status
        // كود تجريبي لاستخراج الـ Payload:
        var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
        
        var command = new ProcessPaymentWebhookCommand
        {
            TransactionReference = "st_extracted_reference_from_json",
            GatewayStatus = "SUCCESS" // أو بناء على الـ event.type الخاص بـ stripe زي "payment_intent.succeeded"
        };

        await _mediator.Send(command);
        return Ok(); // لازم نرد بـ 200 لـ Stripe عشان ميبعتش الإشعار تاني
    }

    [HttpPost("paymob")]
    public async Task<IActionResult> HandlePaymobWebhook([FromQuery] string hmac, [FromBody] JsonElement payload)
    {
        // الـ Paymob بيبعت الـ Data كـ JSON ومعه حماية HMAC في الـ Query String لتأمين الـ Request
        
        var command = new ProcessPaymentWebhookCommand
        {
            TransactionReference = payload.GetProperty("obj").GetProperty("id").ToString(),
            GatewayStatus = payload.GetProperty("obj").GetProperty("success").GetBoolean() ? "SUCCESS" : "FAILED"
        };

        await _mediator.Send(command);
        return Ok();
    }
}