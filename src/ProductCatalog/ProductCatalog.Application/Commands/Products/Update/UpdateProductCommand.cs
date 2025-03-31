using Common.Application.Models;
using MediatR;
using ProductCatalog.Application.Models.Products;
using ProductCatalog.Domain.Contracts;
using ProductCatalog.Domain.Models.Products;

namespace ProductCatalog.Application.Commands.Products.Update;

public class UpdateProductCommand : IRequest<Result>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Status { get; set; }
    public MoneyRequest Price { get; set; }
    public WeightRequest Weight { get; set; }
    public int BrandId { get; set; }
    public int CategoryId { get; set; }
    public int SupplierId { get; set; }

    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Result>
    {
        private readonly IProductRepository _productRepository;

        public UpdateProductCommandHandler(
            IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Result> Handle(
            UpdateProductCommand request,
            CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.Id, cancellationToken);

            if (product is null)
                return Result.Failure(null);

            product.Update(
                request.Name,
                request.Description,
                Money.From(request.Price.Amount),
                request.BrandId,
                request.CategoryId,
                request.SupplierId
            );

            await _productRepository.SaveAsync(product, cancellationToken);

            return Result.Success;
        }
    }
}