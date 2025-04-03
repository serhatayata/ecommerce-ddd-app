using FluentValidation;

namespace Shipping.Application.Commands.ShipmentCompanies.Create;

public class CreateShipmentCompanyCommandValidator : AbstractValidator<CreateShipmentCompanyCommand>
{
    public CreateShipmentCompanyCommandValidator()
    {
        RuleFor(b => b.Name)
            .NotEmpty().WithMessage("Name required");

        RuleFor(b => b.Code)
            .NotEmpty().WithMessage("Code required");
    }
}