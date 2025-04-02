using AutoMapper;
using MediatR;
using ProductCatalog.Application.Queries.Suppliers.Common;
using ProductCatalog.Domain.Contracts;

namespace ProductCatalog.Application.Queries.Suppliers.Details;

public class SupplierDetailsQuery : IRequest<SupplierResponse>
{
    public int Id { get; set; }

    public class SupplierDetailsQueryHandler : IRequestHandler<SupplierDetailsQuery, SupplierResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public SupplierDetailsQueryHandler(
            IProductRepository productRepository,
            IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<SupplierResponse> Handle(
            SupplierDetailsQuery request,
            CancellationToken cancellationToken)
        {
            var supplier = await _productRepository.GetSupplierByIdAsync(request.Id, cancellationToken);
            if (supplier == null) return null;

            var supplierResponse = _mapper.Map<SupplierResponse>(supplier);
            return supplierResponse;
        }
    }
}