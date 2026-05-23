using Logistics.Domain.Common;
using Logistics.Domain.Enums.Chat;

namespace Logistics.Domain.Entities;

public class ChatRoom : BaseEntity
{
    public Guid OrderId { get; private set; }
    public Guid CustomerId { get; private set; }
    public bool IsActive { get; private set; }
    public Customer Customer { get; private set; } = null!;
    private readonly List<ChatMessage> _messages = new();
    public IReadOnlyCollection<ChatMessage> Messages => _messages.AsReadOnly();

    public ChatRoom(Guid orderId, Guid customerId)
    {
        OrderId = orderId;
        CustomerId = customerId;
        IsActive = true;
    }

    public void AddMessage(string senderId, SenderType type, string text)
    {
        _messages.Add(new ChatMessage(senderId, type, text));
    }
    public void CloseRoom()
    {
        IsActive = false;
        AddMessage("SYSTEM", SenderType.Support, "This conversation has been closed.");
    }
}
