using Logistics.Application.Features.Shipment.Commands.Admin;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Logistics.API.Controllers;

public class TrackingController : ApiControllerBase
{
    private readonly IMediator _mediator;

    public TrackingController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // Endpoint يستقبل إحداثيات السائق الحالية ويرسلها للـ Handler
    [HttpPost("update-location")]
    public async Task<IActionResult> UpdateLocation([FromBody] UpdateLiveLocationCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(new { Success = result, Message = "تم تحديث الموقع الحي وبثه للمشتركين." });
    }   
}