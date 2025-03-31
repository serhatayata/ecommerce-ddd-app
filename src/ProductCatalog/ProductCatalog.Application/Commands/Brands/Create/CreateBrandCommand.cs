using MediatR;
using ProductCatalog.Domain.Contracts;
using ProductCatalog.Domain.Models.Brands;

namespace ProductCatalog.Application.Commands.Brands.Create;

public class CreateBrandCommand : IRequest<CreateBrandResponse>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Website { get; set; }

    public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, CreateBrandResponse>
    {
        private readonly IProductRepository _productRepository;

        public CreateBrandCommandHandler(
        IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<CreateBrandResponse> Handle(
        CreateBrandCommand request, 
        CancellationToken cancellationToken)
        {
            var brand = new Brand(
                request.Name,
                request.Description,
                request.Website
            );
            
            await _productRepository.SaveBrandAsync(brand, cancellationToken);

            return new CreateBrandResponse(brand.Id);
        }
    }
}