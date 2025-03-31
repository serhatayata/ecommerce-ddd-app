using Common.Domain.Repositories;
using ProductCatalog.Domain.Models.Brands;
using ProductCatalog.Domain.Models.Products;

namespace ProductCatalog.Domain.Contracts;

public interface IProductRepository : IRepository<Product>
{
    #region Brands
    Task<Brand> SaveBrandAsync(Brand brand, CancellationToken cancellationToken = default);
    Task DeleteBrandAsync(int id, CancellationToken cancellationToken = default);
    #endregion
}