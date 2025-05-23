using Common.Application.Models;
using MediatR;
using ProductCatalog.Domain.Contracts;

namespace ProductCatalog.Application.Commands.Brands.Delete;

public class DeleteBrandCommand : IRequest<Result>
{
    public int Id { get; set; }

    public class DeleteBrandCommandHandler : IRequestHandler<DeleteBrandCommand, Result>
    {
        private readonly IProductRepository _productRepository;

        public DeleteBrandCommandHandler(
        IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Result> Handle(
        DeleteBrandCommand request, 
        CancellationToken cancellationToken)
        {
            await _productRepository.DeleteBrandAsync(request.Id, cancellationToken);
            return Result.Success;   
        }
    }
}