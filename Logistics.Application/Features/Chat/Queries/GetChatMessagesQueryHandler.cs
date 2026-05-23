using Logistics.Application.DTOs;
using Logistics.Application.Interfaces;
using MediatR;

namespace Logistics.Application.Features.Chat.Queries;

public class GetChatMessagesQueryHandler : IRequestHandler<GetChatMessagesQuery, List<MessageDto>>
{
    private readonly IChatRoomRepository _chatRoomRepository;

    public GetChatMessagesQueryHandler(IChatRoomRepository chatRoomRepository)
    {
        _chatRoomRepository = chatRoomRepository;
    }

    public async Task<List<MessageDto>> Handle(GetChatMessagesQuery request, CancellationToken cancellationToken)
    {
        var room = await _chatRoomRepository.GetByIdWithMessagesAsync(request.ChatRoomId, cancellationToken);
        if (room == null) return new List<MessageDto>();

        // لتعليم الرسائل كـ Read لما العميل أو الدعم يفتح الشات
        foreach(var msg in room.Messages.Where(m => !m.IsRead))
        {
            msg.MarkAsRead();
        }

        return room.Messages
            .OrderBy(m => m.SentAt)
            .Select(m => new MessageDto(m.Id, m.SenderId, m.SenderType, m.MessageText, m.SentAt, m.IsRead))
            .ToList();
    }
}