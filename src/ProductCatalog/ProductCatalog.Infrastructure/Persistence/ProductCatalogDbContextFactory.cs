using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using MediatR;

namespace ProductCatalog.Infrastructure.Persistence;

public class ProductCatalogDbContextFactory : IDesignTimeDbContextFactory<ProductCatalogDbContext>
{
    private readonly IMediator _mediator;

    public ProductCatalogDbContextFactory(IMediator mediator)
    {
        _mediator = mediator;
    }

    public ProductCatalogDbContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json",
                         optional: false,
                         reloadOnChange: true)
            .Build();

        var builder = new DbContextOptionsBuilder<ProductCatalogDbContext>();
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        builder.UseSqlServer(
            connectionString,
            sqlOptions => sqlOptions
                .MigrationsHistoryTable("__EFMigrationsHistory", "productcatalog")
                .EnableRetryOnFailure(
                    maxRetryCount: 10,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null)
                .MigrationsAssembly(
                    typeof(ProductCatalogDbContext).Assembly.FullName));

        return new ProductCatalogDbContext(builder.Options, _mediator);
    }
}