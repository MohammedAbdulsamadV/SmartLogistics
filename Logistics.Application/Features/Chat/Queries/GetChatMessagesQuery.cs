using Logistics.Application.DTOs;
using MediatR;

namespace Logistics.Application.Features.Chat.Queries;

public record GetChatMessagesQuery(Guid ChatRoomId) : IRequest<List<MessageDto>>;