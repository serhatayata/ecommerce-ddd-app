using Common.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Domain.Contracts;
using ProductCatalog.Domain.Models.Brands;
using ProductCatalog.Domain.Models.Products;
using ProductCatalog.Infrastructure.Persistence;

namespace ProductCatalog.Infrastructure.Repositories.Products;

public class ProductRepository : EfRepository<Product, ProductCatalogDbContext>, IProductRepository
{
    private readonly ProductCatalogDbContext _dbContext;

    public ProductRepository(
    ProductCatalogDbContext dbContext) 
    : base(dbContext)
    {
        _dbContext = dbContext;
    }

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
    #endregion
}