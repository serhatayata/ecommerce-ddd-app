using System.Reflection;
using Common.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace OrderManagement.Domain;

public static class OrderDomainConfiguration
{
    public static IServiceCollection AddIdentityDomain(
    this IServiceCollection services)
    {
        return services
                .AddInitialData(Assembly.GetExecutingAssembly());  
    }
    
    private static IServiceCollection AddInitialData(
    this IServiceCollection services,
    Assembly assembly)
    {
        services
            .Scan(scan => scan
                .FromAssemblies(assembly)
                .AddClasses(classes => classes
                    .AssignableTo(typeof(IInitialData)))
                .AsImplementedInterfaces()
                .WithTransientLifetime());

        return services;
    }
}