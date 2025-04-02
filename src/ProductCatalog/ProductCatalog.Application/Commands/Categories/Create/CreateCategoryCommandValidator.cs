using FluentValidation;

namespace ProductCatalog.Application.Commands.Categories.Create;

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(b => b.Name)
            .NotEmpty().WithMessage("Name required");

        RuleFor(b => b.Description)
            .NotEmpty().WithMessage("Description required");
    }
}