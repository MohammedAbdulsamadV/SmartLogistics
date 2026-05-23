using Logistics.Domain.Enums.Chat;

namespace Logistics.Application.DTOs;

public record MessageDto(Guid Id, string SenderId, SenderType SenderType, string MessageText, DateTime SentAt, bool IsRead);