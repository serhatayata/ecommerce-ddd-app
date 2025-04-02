using Common.Application.Models;
using MediatR;
using ProductCatalog.Domain.Contracts;

namespace ProductCatalog.Application.Commands.Categories.Delete;

public class DeleteCategoryCommand  : IRequest<Result>
{
    public int Id { get; set; }

    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Result>
    {
        private readonly IProductRepository _productRepository;

        public DeleteCategoryCommandHandler(
        IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Result> Handle(
        DeleteCategoryCommand request, 
        CancellationToken cancellationToken)
        {
            await _productRepository.DeleteCategoryAsync(request.Id, cancellationToken);
            return Result.Success;   
        }
    }
}