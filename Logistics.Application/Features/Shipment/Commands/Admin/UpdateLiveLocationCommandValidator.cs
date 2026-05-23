using FluentValidation;

namespace Logistics.Application.Features.Shipment.Commands.Admin;

public class UpdateLiveLocationCommandValidator : AbstractValidator<UpdateLiveLocationCommand>
{
    public UpdateLiveLocationCommandValidator()
    {
        RuleFor(x => x.ShipmentId)
            .NotEmpty().WithMessage("رقم الشحنة مطلوب ولا يمكن أن يكون فارغاً.");

        RuleFor(x => x.Latitude)
            .InclusiveBetween(-90m, 90m).WithMessage("إحداثيات خط العرض (Latitude) يجب أن تكون بين -90 و 90.");

        RuleFor(x => x.Longitude)
            .InclusiveBetween(-180m, 180m).WithMessage("إحداثيات خط الطول (Longitude) يجب أن تكون بين -180 و 180.");
    }
}