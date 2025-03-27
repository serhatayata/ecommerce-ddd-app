using Common.Infrastructure.Repositories;
using ProductCatalog.Domain.Contracts;
using ProductCatalog.Domain.Models.Products;
using ProductCatalog.Infrastructure.Persistence;

namespace ProductCatalog.Infrastructure.Repositories.Products;

public class ProductRepository : EfRepository<Product, ProductCatalogDbContext>, IProductRepository
{
    public ProductRepository(
    ProductCatalogDbContext dbContext) 
    : base(dbContext)
    {
    }
}