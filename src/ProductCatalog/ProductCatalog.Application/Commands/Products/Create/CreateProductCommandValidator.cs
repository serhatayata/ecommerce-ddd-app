using FluentValidation;

namespace ProductCatalog.Application.Commands.Products.Create;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(b => b.Name)
            .NotEmpty().WithMessage("Name required");

        RuleFor(b => b.Description)
            .NotEmpty().WithMessage("Description required");

        RuleFor(b => b.Price.Amount)
            .NotEmpty().WithMessage("Price required.");

        RuleFor(b => b.Weight.Value)
            .NotEmpty().WithMessage("Weight value required.");

        RuleFor(b => b.Weight.Unit)
            .NotEmpty().WithMessage("Weight unit required.");
    }
}