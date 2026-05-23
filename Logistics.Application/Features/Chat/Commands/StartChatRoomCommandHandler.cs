using Logistics.Application.Interfaces;
using Logistics.Domain.Entities;
using MediatR;

namespace Logistics.Application.Features.Chat.Commands;

public class StartChatRoomCommandHandler: IRequestHandler<StartChatRoomCommand, Guid>
{
    private readonly IGenericRepository<ChatRoom> _chatRoomRepository;
    private readonly IUnitOfWork _unitOfWork;

    public StartChatRoomCommandHandler(IGenericRepository<ChatRoom> chatRoomRepository, IUnitOfWork unitOfWork)
    {
        _chatRoomRepository = chatRoomRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(StartChatRoomCommand request, CancellationToken cancellationToken)
    {
        var chatRoom = new ChatRoom(request.OrderId, request.CustomerId);
        await _chatRoomRepository.AddAsync(chatRoom);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return chatRoom.Id;
    }
}