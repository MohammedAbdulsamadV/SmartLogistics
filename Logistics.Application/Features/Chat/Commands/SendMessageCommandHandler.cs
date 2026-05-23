using Logistics.Application.Interfaces;
using MediatR;

namespace Logistics.Application.Features.Chat.Commands;

public class SendMessageCommandHandler : IRequestHandler<SendMessageCommand, bool>
{
    private readonly IChatRoomRepository _chatRoomRepository; // ريبوزيتوري مخصص لقراءة الـ Aggregate بالـ Messages بتاعته
    private readonly IUnitOfWork _unitOfWork;

    public SendMessageCommandHandler(IChatRoomRepository chatRoomRepository, IUnitOfWork unitOfWork)
    {
        _chatRoomRepository = chatRoomRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(SendMessageCommand request, CancellationToken cancellationToken)
    {
        var room = await _chatRoomRepository.GetByIdWithMessagesAsync(request.ChatRoomId, cancellationToken);
        if (room == null) throw new Exception("أوضة المحادثة غير موجودة.");
        if (!room.IsActive) throw new Exception("لا يمكن إرسال رسالة في محادثة مغلقة.");

        room.AddMessage(request.SenderId, request.SenderType, request.MessageText);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}