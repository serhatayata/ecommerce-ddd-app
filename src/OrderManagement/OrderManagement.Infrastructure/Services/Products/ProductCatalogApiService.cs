using Common.Application.Models.Responses.Products;
using Common.Infrastructure.Extensions;
using OrderManagement.Application.Services.Products;

namespace OrderManagement.Infrastructure.Services.Products;

public class ProductCatalogApiService : IProductCatalogApiService
{
    private readonly HttpClient _productCatalogApiClient;

    public ProductCatalogApiService(
    IHttpClientFactory httpClientFactory)
    {
        _productCatalogApiClient = httpClientFactory.CreateClient("product-catalog");
    }

    public async Task<ProductPriceListResponse?> GetProductsPriceByIds(
    int[] ids, 
    CancellationToken cancellationToken)
        => await _productCatalogApiClient.PostAsync<ProductPriceListResponse, int []>(
            "product/products-price-by-ids", 
            ids,
            cancellationToken);
}