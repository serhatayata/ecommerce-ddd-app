using Common.Application.Models.Responses.Products;

namespace OrderManagement.Application.Services.Products;

public interface IProductCatalogApiService
{
    public Task<ProductPriceListResponse?> GetProductsPriceByIds(int[] ids, CancellationToken cancellationToken);
}