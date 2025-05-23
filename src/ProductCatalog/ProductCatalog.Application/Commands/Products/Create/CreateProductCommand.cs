using Common.Domain.ValueObjects;
using MediatR;
using ProductCatalog.Application.Models.Products;
using ProductCatalog.Domain.Contracts;
using ProductCatalog.Domain.Models.Products;

namespace ProductCatalog.Application.Commands.Products.Create;

public class CreateProductCommand : IRequest<CreateProductResponse>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int Status { get; set; }
    public MoneyRequest Price { get; set; }
    public WeightRequest Weight { get; set; }
    public int BrandId { get; set; }
    public int CategoryId { get; set; }
    public int SupplierId { get; set; }

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CreateProductResponse>
    {
        private readonly IProductRepository _productRepository;

        public CreateProductCommandHandler(
        IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<CreateProductResponse> Handle(
        CreateProductCommand request, 
        CancellationToken cancellationToken)
        {
            var product = Product.Create(
                request.Name,
                request.Description,
                Money.From(request.Price.Amount),
                request.BrandId,
                request.CategoryId,
                request.SupplierId
            );
            
            await _productRepository.SaveAsync(product, cancellationToken);

            return new CreateProductResponse(product.Id);
        }
    }
}