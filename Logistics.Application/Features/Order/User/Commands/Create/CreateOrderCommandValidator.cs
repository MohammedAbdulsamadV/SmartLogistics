using FluentValidation;

namespace Logistics.Application.Features.Order.User.Commands.Create;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Email address is not in correct format.");
        RuleFor(x => x.TrackingNumber).NotEmpty().WithMessage("The tracking number is required.")
            .MaximumLength(50).WithMessage("The tracking number cannot exceed 50 characters.");
        RuleFor(x => x.ShippingAddress).NotNull().WithMessage("The shipping address is required.");
        RuleFor(x => x.ShippingAddress.City).NotNull().WithMessage("The city is required.");
        RuleFor(x => x.ShippingAddress.Country).NotNull().WithMessage("The country is required.");
        RuleFor(x => x.ShippingAddress.State).NotNull().WithMessage("The state is required.");
        RuleFor(x => x.ShippingAddress.ZipCode).NotNull().WithMessage("The zip code is required.").Matches(@"^\d{5}$").WithMessage("Invalid zip code.");
        RuleFor(x => x.OrderItems).NotEmpty().WithMessage("The order must have at least one item.");
        RuleForEach(x => x.OrderItems).ChildRules(item =>
        {
            item.RuleFor(i => i.ProductName)
                .NotEmpty().WithMessage("Product name is required.");

            item.RuleFor(i => i.Price)
                .GreaterThan(0).WithMessage("Unit price must be greater than zero.");

            item.RuleFor(i => i.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than zero.");
        });


    }
}