using FluentValidation;

namespace Logistics.Application.Features.Chat.Commands;

public class SendMessageCommandValidator : AbstractValidator<SendMessageCommand>
{
    public SendMessageCommandValidator()
    {
        RuleFor(x => x.ChatRoomId).NotEmpty().WithMessage("Chat room id is required");
        RuleFor(x => x.SenderId).NotEmpty().WithMessage("Sender id is required");
        RuleFor(x => x.MessageText)
            .NotEmpty().WithMessage("Message text cannot be empty");
            .MaximumLength(2000).WithMessage("Message text cannot be longer than 2000 characters");
        RuleFor(x => x.SenderType).IsInEnum().WithMessage("Sender type is invalid");
    }
}