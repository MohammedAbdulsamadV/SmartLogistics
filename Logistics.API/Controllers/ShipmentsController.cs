using Logistics.Application.Features.Shipment.Commands.Admin;
using Logistics.Application.Features.Shipment.Queries.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Logistics.API.Controllers;

public class ShipmentsController : ApiControllerBase
{
    private readonly IMediator _mediator;

    public ShipmentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateShipmentCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPut("location")]
    public async Task<IActionResult> UpdateLocation([FromBody] UpdateLiveLocationCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPut("complete-current-leg")]
    public async Task<IActionResult> CompleteLeg([FromBody] CompleteCurrentLegCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDetails(Guid id)
    {
        var query = new GetShipmentDetailsQuery { ShipmentId = id };
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}