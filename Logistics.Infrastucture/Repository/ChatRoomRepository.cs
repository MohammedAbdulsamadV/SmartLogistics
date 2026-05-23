using Logistics.Application.Interfaces;
using Logistics.Domain.Entities;
using Logistics.Infrastucture.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Logistics.Infrastucture.Repository;

public class ChatRoomRepository : GenericRepository<ChatRoom> , IChatRoomRepository
    
{
    public ChatRoomRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<ChatRoom?> GetByIdWithMessagesAsync(Guid chatRoomId, CancellationToken token)
    {
        return  _Context.ChatRooms.Include(e => e.Messages).FirstOrDefault(e => e.Id == chatRoomId);
    }
}