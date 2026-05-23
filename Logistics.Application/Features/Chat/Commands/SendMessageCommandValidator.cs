using FluentValidation;

namespace Logistics.Application.Features.Chat.Commands;

public class SendMessageCommandValidator : AbstractValidator<SendMessageCommand>
{
    public SendMessageCommandValidator()
    {
        RuleFor(x => x.ChatRoomId).NotEmpty().WithMessage("معرف غرفة المحادثة مطلوب.");
        RuleFor(x => x.SenderId).NotEmpty().WithMessage("معرف المرسل مطلوب.");
        RuleFor(x => x.MessageText)
            .NotEmpty().WithMessage("لا يمكن إرسال رسالة فارغة.")
            .MaximumLength(2000).WithMessage("الرسالة طويلة جداً (الحد الأقصى 2000 حرف).");
        RuleFor(x => x.SenderType).IsInEnum().WithMessage("نوع المرسل غير صحيح.");
    }
}