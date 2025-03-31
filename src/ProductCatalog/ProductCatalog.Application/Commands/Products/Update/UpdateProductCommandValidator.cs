using FluentValidation;

namespace ProductCatalog.Application.Commands.Products.Update;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(b => b.Id)
            .NotEmpty().WithMessage("Id required");

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