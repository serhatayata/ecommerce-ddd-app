using Common.Domain.Repositories;
using ProductCatalog.Domain.Models.Brands;
using ProductCatalog.Domain.Models.Categories;
using ProductCatalog.Domain.Models.Products;
using ProductCatalog.Domain.Models.Suppliers;

namespace ProductCatalog.Domain.Contracts;

public interface IProductRepository : IRepository<Product>
{
    Task<List<Product>> GetProductsByIdsAsync(int[] ids, CancellationToken cancellationToken = default);

    #region Brands
    Task<Brand> SaveBrandAsync(Brand brand, CancellationToken cancellationToken = default);
    Task DeleteBrandAsync(int id, CancellationToken cancellationToken = default);
    Task<Brand> GetBrandByIdAsync(int id, CancellationToken cancellationToken = default);
    #endregion
    #region Categories
    Task<Category> SaveCategoryAsync(Category category, CancellationToken cancellationToken = default);
    Task DeleteCategoryAsync(int id, CancellationToken cancellationToken = default);
    Task<Category> GetCategoryByIdAsync(int id, CancellationToken cancellationToken = default);
    #endregion
    #region Suppliers
    Task<Supplier> SaveSupplierAsync(Supplier supplier, CancellationToken cancellationToken = default);
    Task DeleteSupplierAsync(int id, CancellationToken cancellationToken = default);
    Task<Supplier> GetSupplierByIdAsync(int id, CancellationToken cancellationToken = default);
    #endregion
}