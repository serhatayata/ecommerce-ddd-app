using FluentValidation;

namespace Shipping.Application.Commands.Shipments.Create;

public class CreateShipmentCommandValidator : AbstractValidator<CreateShipmentCommand>
{
    public CreateShipmentCommandValidator()
    {
        RuleFor(x => x.OrderId)
            .GreaterThan(0)
            .WithMessage("Order ID must be greater than 0");

        RuleFor(x => x.Street)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.City)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.State)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Country)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.ZipCode)
            .NotEmpty()
            .MaximumLength(20);

        RuleFor(x => x.TrackingNumber)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.ShipmentCompanyId)
            .GreaterThan(0)
            .WithMessage("Shipment Company ID must be greater than 0");

        RuleFor(x => x.Status)
            .IsInEnum()
            .WithMessage("Invalid shipment status");
    }
}