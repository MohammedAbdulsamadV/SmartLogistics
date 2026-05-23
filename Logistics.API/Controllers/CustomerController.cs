using Logistics.Application.Features.Customer;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Logistics.API.Controllers;

public class CustomerController : ApiControllerBase
{ 
    private readonly IMediator _mediator;
    public CustomerController(IMediator mediator) => _mediator = mediator;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCustomerCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(new { CustomerId = id, Message = "Customer Created" });
    }
}