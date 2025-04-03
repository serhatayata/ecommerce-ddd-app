using System.Reflection;
using Common.Domain.Repositories;
using Common.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shipping.Domain.Contracts;
using Shipping.Infrastructure.Persistence;
using Shipping.Infrastructure.Repositories.Shipments;

namespace Shipping.Infrastructure;

public static class ShippingInfrastructureConfiguration
{
    public static IServiceCollection AddShippingInfrastructure(
    this IServiceCollection services,
    IConfiguration configuration)
    {
        services
            .AddDatabase(configuration)
            .AddRepositories()
            .AddTransient<IDbInitializer, ShippingDbInitializer>()
            .AddTransient<IShipmentRepository, ShipmentRepository>();

        return services;
    }

    private static IServiceCollection AddDatabase(
    this IServiceCollection services,
    IConfiguration configuration)
    {
        services
            .AddScoped<DbContext, ShippingDbContext>()
            .AddDbContext<ShippingDbContext>(options => options
                .UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    sqlOptions => sqlOptions
                        .MigrationsHistoryTable("__EFMigrationsHistory", "shipping")
                        .EnableRetryOnFailure(
                            maxRetryCount: 10,
                            maxRetryDelay: TimeSpan.FromSeconds(30),
                            errorNumbersToAdd: null)
                        .MigrationsAssembly(
                            typeof(ShippingDbContext).Assembly.FullName)));

        return services;
    }

    private static IServiceCollection AddRepositories(
    this IServiceCollection services)
    {
        services
            .Scan(scan => scan
                .FromAssemblies(Assembly.GetExecutingAssembly())
                .AddClasses(classes => classes
                    .AssignableTo(typeof(IRepository<>)))
                .AsImplementedInterfaces()
                .WithTransientLifetime());

        return services;
    }
}