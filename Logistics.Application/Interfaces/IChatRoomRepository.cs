using Logistics.Domain.Entities;

namespace Logistics.Application.Interfaces;

public interface IChatRoomRepository : IGenericRepository<ChatRoom>
{
    Task<ChatRoom> GetByIdWithMessagesAsync(Guid chatRoomId,CancellationToken token);
}