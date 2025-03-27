using MediatR;
using ProductCatalog.Application.Queries.Products.Common;
using ProductCatalog.Domain.Contracts;
using AutoMapper;

namespace ProductCatalog.Application.Queries.Products.Details;

public class ProductDetailsQuery : IRequest<ProductResponse>
{
    public int Id { get; set; }

    public class ProductDetailsQueryHandler : IRequestHandler<ProductDetailsQuery, ProductResponse>
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;

        public ProductDetailsQueryHandler(
            IProductRepository productRepository,
            IMapper mapper)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
        }

        public async Task<ProductResponse> Handle(
        ProductDetailsQuery request,
        CancellationToken cancellationToken)
        {
            var product = await productRepository.GetByIdAsync(request.Id, cancellationToken);
        
            return mapper.Map<ProductResponse>(product);
        }
    }
}