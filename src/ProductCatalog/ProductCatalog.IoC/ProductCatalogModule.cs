using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductCatalog.Infrastructure;
using ProductCatalog.Domain;

namespace ProductCatalog.IoC;

public static class ProductCatalogModule
{
    public static void Register(
    this IServiceCollection services,
    IConfiguration configuration)
    {
        // services.AddProductCatalogApplication(configuration)
        services.AddProductCatalogInfrastructure(configuration)
                .AddProductCatalogDomain();
    }
}