using System.Reflection;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Domain.Models.Brands;
using ProductCatalog.Domain.Models.Categories;
using ProductCatalog.Domain.Models.Products;
using ProductCatalog.Domain.Models.Suppliers;

namespace ProductCatalog.Infrastructure.Persistence;

public class ProductCatalogDbContext : DbContext
{
    public ProductCatalogDbContext(
    DbContextOptions<ProductCatalogDbContext> options)
    : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.HasDefaultSchema("productcatalog");

        base.OnModelCreating(modelBuilder);
    }
}