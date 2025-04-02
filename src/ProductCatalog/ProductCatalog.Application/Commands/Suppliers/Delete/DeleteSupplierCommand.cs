using Common.Application.Models;
using MediatR;
using ProductCatalog.Domain.Contracts;

namespace ProductCatalog.Application.Commands.Suppliers.Delete;

public class DeleteSupplierCommand : IRequest<Result>
{
    public int Id { get; set; }

    public class DeleteSupplierCommandHandler : IRequestHandler<DeleteSupplierCommand, Result>
    {
        private readonly IProductRepository _productRepository;

        public DeleteSupplierCommandHandler(
        IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Result> Handle(
        DeleteSupplierCommand request, 
        CancellationToken cancellationToken)
        {
            await _productRepository.DeleteSupplierAsync(request.Id, cancellationToken);
            return Result.Success;
        }
    }
}