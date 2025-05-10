using System.Reflection;
using Common.Domain.Repositories;
using Common.Infrastructure.Persistence;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderManagement.Application.Services.Products;
using OrderManagement.Domain.Contracts;
using OrderManagement.Infrastructure.Persistence;
using OrderManagement.Infrastructure.Repositories;
using OrderManagement.Infrastructure.Services.Products;

namespace OrderManagement.Infrastructure;

public static class OrderInfrastructureConfiguration
{
    public static IServiceCollection AddOrderManagementInfrastructure(
    this IServiceCollection services,
    IConfiguration configuration)
    {
        services
            .AddDatabase(configuration)
            .AddRepositories()
            .AddTransient<IDbInitializer, OrderDbInitializer>()
            .AddTransient<IOrderRepository, OrderRepository>()
            .AddTransient<IProductCatalogApiService, ProductCatalogApiService>()
            .AddQueueConfigurations();

        return services;
    }

    private static IServiceCollection AddDatabase(
    this IServiceCollection services,
    IConfiguration configuration)
    {
        services
            .AddScoped<DbContext, OrderDbContext>()
            .AddDbContext<OrderDbContext>(options => options
                .UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    sqlOptions => sqlOptions
                        .EnableRetryOnFailure(
                            maxRetryCount: 10,
                            maxRetryDelay: TimeSpan.FromSeconds(30),
                            errorNumbersToAdd: null)
                        .MigrationsAssembly(
                            typeof(OrderDbContext).Assembly.FullName)
                        .MigrationsHistoryTable("__EFMigrationsHistory", "Order")));

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

    private static IServiceCollection AddQueueConfigurations(
    this IServiceCollection services)
    {
        return services.AddMassTransit(m =>
        {
            m.UsingRabbitMq((context, cfg) =>
            {

            });
        });
    }
}