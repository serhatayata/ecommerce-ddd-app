using Common.Application.Models;
using MediatR;

namespace ProductCatalog.Application.Commands.Brands.Delete;

public class DeleteBrandCommand : IRequest<Result>
{
    public int Id { get; set; }

    public class DeleteBrandCommandHandler : IRequestHandler<DeleteBrandCommand, Result>
    {
        public async Task<Result> Handle(
        DeleteBrandCommand request, 
        CancellationToken cancellationToken)
        {
            
        }
    }
}