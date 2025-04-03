using System.Reflection;
using Common.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Shipping.Domain;

public static class ShippingDomainConfiguration
{
    public static IServiceCollection AddShippingDomain(
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