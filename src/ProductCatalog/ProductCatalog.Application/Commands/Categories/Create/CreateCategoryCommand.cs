using MediatR;
using ProductCatalog.Domain.Contracts;
using ProductCatalog.Domain.Models.Categories;

namespace ProductCatalog.Application.Commands.Categories.Create;

public class CreateCategoryCommand : IRequest<CreateCategoryResponse>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int? ParentId { get; set; }

    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CreateCategoryResponse>
    {
        private readonly IProductRepository _productRepository;

        public CreateCategoryCommandHandler(
        IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<CreateCategoryResponse> Handle(
        CreateCategoryCommand request, 
        CancellationToken cancellationToken)
        {
            var category = Category.Create(
                request.Name, 
                request.Description, 
                request.ParentId
            );

            await _productRepository.SaveCategoryAsync(category, cancellationToken);

            return new CreateCategoryResponse(category.Id);
        }
    }
}