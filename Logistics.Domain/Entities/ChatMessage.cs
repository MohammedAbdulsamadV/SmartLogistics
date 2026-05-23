using Logistics.Domain.Common;
using Logistics.Domain.Enums.Chat;

namespace Logistics.Domain.Entities;

public class ChatMessage : BaseEntity
{
    public string SenderId { get; private set; }
    public SenderType SenderType { get; private set; }
    public string MessageText { get; private set; }
    public DateTime SentAt { get; private set; } = DateTime.UtcNow;
    public bool IsRead { get; private set; }
    public Guid ChatRoomId { get; private set; } // عشان نربطها بالـ Room في الداتابيز
    public ChatMessage(string senderId, SenderType type, string text)
    {
        SenderId = senderId;
        SenderType = type;
        MessageText = text;
        SentAt = DateTime.UtcNow;
        IsRead = false;
    }

    public void MarkAsRead() => IsRead = true;
}