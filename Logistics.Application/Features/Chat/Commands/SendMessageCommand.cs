using Logistics.Domain.Enums.Chat;
using MediatR;

namespace Logistics.Application.Features.Chat.Commands;

public record SendMessageCommand(Guid ChatRoomId, string SenderId, SenderType SenderType, string MessageText) : IRequest<bool>;