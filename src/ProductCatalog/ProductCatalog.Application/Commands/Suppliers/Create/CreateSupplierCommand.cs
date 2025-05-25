using MediatR;
using ProductCatalog.Domain.Contracts;
using ProductCatalog.Domain.Models.Suppliers;

namespace ProductCatalog.Application.Commands.Suppliers.Create;

public class CreateSupplierCommand : IRequest<CreateSupplierResponse>
{
    public string Name { get; set; }
    public string ContactName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Street { get; }
    public string City { get; }
    public string State { get; }
    public string Country { get; }
    public string ZipCode { get; }

    public class CreateSupplierCommandHandler : IRequestHandler<CreateSupplierCommand, CreateSupplierResponse>
    {
        private readonly IProductRepository _productRepository;

        public CreateSupplierCommandHandler(
        IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<CreateSupplierResponse> Handle(
        CreateSupplierCommand request, 
        CancellationToken cancellationToken)
        {
            var supplier = Supplier.Create(
                request.Name,
                request.ContactName,
                request.Email,
                request.Phone,
                Address.From(
                    request.Street,
                    request.City,
                    request.State,
                    request.Country,
                    request.ZipCode
                )
            );

            await _productRepository.SaveSupplierAsync(supplier, cancellationToken);

            return new CreateSupplierResponse(supplier.Id);
        }
    }
}