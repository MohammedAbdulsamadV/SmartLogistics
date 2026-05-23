using Logistics.Application.Features.Chat.Commands;
using Logistics.Application.Features.Chat.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Logistics.API.Controllers;

public class ChatsController : ApiControllerBase
{
    private readonly IMediator _mediator;
    public ChatsController(IMediator mediator) => _mediator = mediator;

    // 1. فتح غرفة شات جديدة مرتبطة بأوردر وعميل
    [HttpPost("rooms")]
    public async Task<IActionResult> StartRoom([FromBody] StartChatRoomCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(new { ChatRoomId = id, Message = "تم فتح غرفة المحادثة بنجاح." });
    }

    // 2. إرسال رسالة جديدة جوه الشات
    [HttpPost("rooms/{roomId}/messages")]
    public async Task<IActionResult> SendMessage(Guid roomId, [FromBody] SendMessageCommand command)
    {
        if (roomId != command.ChatRoomId) return BadRequest("غرفة المحادثة غير متطابقة.");
        
        var result = await _mediator.Send(command);
        return Ok(new { Success = result, Message = "تم إرسال الرسالة." });
    }

    // 3. جلب جميع الرسائل داخل شات معين وعمل Mark As Read
    [HttpGet("rooms/{roomId}/messages")]
    public async Task<IActionResult> GetMessages(Guid roomId)
    {
        var result = await _mediator.Send(new GetChatMessagesQuery(roomId));
        return Ok(result);
    }
}