using Logistics.Application.Features.Order.Admin.Queries.GetAllOrdersQuery;
using Logistics.Application.Features.Order.User.Commands.Create;
using Microsoft.AspNetCore.Mvc;

namespace Logistics.API.Controllers;

public class OrderController : ApiControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrderCommand command)
    {
        var orderId = await Mediator.Send(command);
        return Ok(orderId);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await Mediator.Send(new GetAllOrders()));
    }
    

}