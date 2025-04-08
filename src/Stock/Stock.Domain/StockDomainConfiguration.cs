using System.Reflection;
using Common.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Stock.Domain;

public static class StockDomainConfiguration
{
    public static IServiceCollection AddStockDomain(
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