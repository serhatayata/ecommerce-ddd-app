using Common.Application.Models.Responses.Products;
using MediatR;
using ProductCatalog.Domain.Contracts;

namespace ProductCatalog.Application.Queries.Products.ProductPrice;

public class GetProductsPriceByIdsQuery : IRequest<ProductPriceListResponse>
{
    public int[] Ids { get; set; }

    public class GetProductsPriceByIdsQueryHandler : IRequestHandler<GetProductsPriceByIdsQuery, ProductPriceListResponse>
    {
        private readonly IProductRepository _productRepository;

        public GetProductsPriceByIdsQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductPriceListResponse> Handle(GetProductsPriceByIdsQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetProductsByIdsAsync(request.Ids, cancellationToken);
            if (products == null || !products.Any())
                return new ProductPriceListResponse
                {
                    List = new List<ProductPriceResponse>()
                };

            var productPrices = products.Select(p => new ProductPriceResponse
            {
                Id = p.Id,
                Price = p.Price
            }).ToList();

            return new ProductPriceListResponse
            {
                List = productPrices
            };
        }
    }
}