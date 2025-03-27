using System.Reflection;
using Common.Infrastructure.DbContexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Domain.Models.Brands;
using ProductCatalog.Domain.Models.Categories;
using ProductCatalog.Domain.Models.Products;
using ProductCatalog.Domain.Models.Suppliers;

namespace ProductCatalog.Infrastructure.Persistence;

public class ProductCatalogDbContext : BaseDbContext<ProductCatalogDbContext>
{
    public ProductCatalogDbContext(
    DbContextOptions<ProductCatalogDbContext> options,
    IMediator mediator)
    : base(options, mediator)
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