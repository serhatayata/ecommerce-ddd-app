using Common.Application.Models;
using MediatR;
using ProductCatalog.Domain.Contracts;

namespace ProductCatalog.Application.Commands.Products.Delete;

public class DeleteProductCommand : IRequest<Result>
{
    public int Id { get; set; }

    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Result>
    {
        private readonly IProductRepository _productRepository;

        public DeleteProductCommandHandler(
        IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Result> Handle(
        DeleteProductCommand request,
        CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.Id, cancellationToken);

            if (product is null)
                return Result.Failure(null);

            await _productRepository.DeleteAsync(product, cancellationToken);

            return Result.Success;
        }
    }
}