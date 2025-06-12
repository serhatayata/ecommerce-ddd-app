using Common.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Domain.Contracts;
using ProductCatalog.Domain.Models.Brands;
using ProductCatalog.Domain.Models.Categories;
using ProductCatalog.Domain.Models.Products;
using ProductCatalog.Domain.Models.Suppliers;
using ProductCatalog.Infrastructure.Persistence;

namespace ProductCatalog.Infrastructure.Repositories.Products;

public class ProductRepository : EfRepository<Product, ProductCatalogDbContext, int>, IProductRepository
{
    private readonly ProductCatalogDbContext _dbContext;

    public ProductRepository(
    ProductCatalogDbContext dbContext) 
    : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Product>> GetProductsByIdsAsync(
    int[] ids, 
    CancellationToken cancellationToken = default)
        =>  await _dbContext.Set<Product>()
                                     .Where(x => ids.Contains(x.Id))
                                     .ToListAsync(cancellationToken);

    #region Brands
    public async Task<Brand> SaveBrandAsync(
    Brand brand, 
    CancellationToken cancellationToken = default)
    {
        await _dbContext.Set<Brand>().AddAsync(brand, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);   

        return brand; 
    }

    public async Task DeleteBrandAsync(
    int id, 
    CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.Set<Brand>()
                                     .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (entity is null)
            return;

        _dbContext.Set<Brand>().Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<Brand> GetBrandByIdAsync(
    int id, 
    CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.Set<Brand>()
                                     .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (entity is null)
            return null!;

        return entity;
    }
    #endregion
    #region Categories
    public async Task<Category> SaveCategoryAsync(
    Category category, 
    CancellationToken cancellationToken = default)
    {
        await _dbContext.Set<Category>().AddAsync(category, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);   

        return category; 
    }

    public async Task DeleteCategoryAsync(
    int id, 
    CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.Set<Category>()
                                     .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (entity is null)
            return;

        _dbContext.Set<Category>().Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);    
    }

    public async Task<Category> GetCategoryByIdAsync(int id, CancellationToken cancellationToken = default)
        =>  await _dbContext.Set<Category>()
                                     .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    #endregion
    #region Suppliers
    public async Task<Supplier> SaveSupplierAsync(
    Supplier supplier, 
    CancellationToken cancellationToken = default)
    {
        await _dbContext.Set<Supplier>().AddAsync(supplier, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);   

        return supplier; 
    }

    public async Task DeleteSupplierAsync(
    int id, 
    CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.Set<Supplier>()
                                     .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (entity is null)
            return;

        _dbContext.Set<Supplier>().Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);    
    }

    public async Task<Supplier> GetSupplierByIdAsync(
    int id, 
    CancellationToken cancellationToken = default)
        =>  await _dbContext.Set<Supplier>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    #endregion
}