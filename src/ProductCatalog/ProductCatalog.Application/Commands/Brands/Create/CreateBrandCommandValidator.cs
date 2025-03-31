using FluentValidation;

namespace ProductCatalog.Application.Commands.Brands.Create;

public class CreateBrandCommandValidator : AbstractValidator<CreateBrandCommand>
{
    public CreateBrandCommandValidator()
    {
        RuleFor(b => b.Name)
            .NotEmpty().WithMessage("Name required");

        RuleFor(b => b.Description)
            .NotEmpty().WithMessage("Description required");

        RuleFor(b => b.Website)
            .NotEmpty().WithMessage("Website required");
    }
}