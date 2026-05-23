using MediatR;

namespace Logistics.Application.Features.Chat.Commands;

public record StartChatRoomCommand(Guid OrderId, Guid CustomerId) : IRequest<Guid>;