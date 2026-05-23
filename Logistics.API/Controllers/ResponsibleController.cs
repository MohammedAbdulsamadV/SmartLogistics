using Logistics.Application.Features.Responsible;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Logistics.API.Controllers;

public class ResponsibleController : ApiControllerBase
{
    private readonly IMediator _mediator;
    public ResponsibleController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AdminCreateResponsibleCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(new 
        { 
            ResponsibleId = id, 
            Message = "تم تسجيل المسؤول بنجاح في النظام." 
        });
    }
    
    [HttpPut("{id}/integration")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateIntegration(Guid id, [FromBody] AdminUpdateShippingIntegrationCommand command)
    {
        command.ResponsibleId = id;
        var result = await _mediator.Send(command);
        return Ok(new { Success = result, Message = "تم تحديث بيانات الربط الخارجي بنجاح." });
    }
    
}