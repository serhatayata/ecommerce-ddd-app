using AutoMapper;
using MediatR;
using ProductCatalog.Application.Queries.Brands.Common;
using ProductCatalog.Domain.Contracts;

namespace ProductCatalog.Application.Queries.Brands.Details;

public class BrandDetailsQuery  : IRequest<BrandResponse>
{
    public int Id { get; set; }

    public class BrandDetailsQueryHandler : IRequestHandler<BrandDetailsQuery, BrandResponse>
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;

        public BrandDetailsQueryHandler(
            IProductRepository productRepository,
            IMapper mapper)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
        }

        public async Task<BrandResponse> Handle(
        BrandDetailsQuery request,
        CancellationToken cancellationToken)
        {
            var brand = await productRepository.GetBrandByIdAsync(request.Id, cancellationToken);

            return mapper.Map<BrandResponse>(brand);
        }
    }
}