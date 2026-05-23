using FluentValidation;

namespace Logistics.Application.Features.Shipment.Commands.Admin;

public class UpdateLiveLocationCommandValidator : AbstractValidator<UpdateLiveLocationCommand>
{
    public UpdateLiveLocationCommandValidator()
    {
        RuleFor(x => x.ShipmentId)
            .NotEmpty().WithMessage("ShipmentId is required.");

        RuleFor(x => x.Latitude)
            .InclusiveBetween(-90m, 90m).WithMessage("Latitude between (-90m, 90m)");

        RuleFor(x => x.Longitude)
            .InclusiveBetween(-180m, 180m).WithMessage("Langitude between (-180m, 180m)");
    }
}