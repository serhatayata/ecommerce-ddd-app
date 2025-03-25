using System.Reflection;
using Common.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ProductCatalog.Domain;

public static class ProductCatalogDomainConfiguration
{
    public static IServiceCollection AddProductCatalogDomain(
    this IServiceCollection services)
        => services.AddInitialData(Assembly.GetExecutingAssembly());  
    
    private static IServiceCollection AddInitialData(
    this IServiceCollection services,
    Assembly assembly)
        => services
            .Scan(scan => scan
                .FromAssemblies(assembly)
                .AddClasses(classes => classes
                    .AssignableTo(typeof(IInitialData)))
                .AsImplementedInterfaces()
                .WithTransientLifetime());
}