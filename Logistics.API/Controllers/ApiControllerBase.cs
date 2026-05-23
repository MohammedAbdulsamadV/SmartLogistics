using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Logistics.API.Controllers;

[ApiController]
[Route("api/[controller]")] 
public abstract class ApiControllerBase : ControllerBase
{
    private ISender? _mediator;

    public ISender? Mediator => _mediator ??= HttpContext.RequestServices.GetService<ISender>();
}
