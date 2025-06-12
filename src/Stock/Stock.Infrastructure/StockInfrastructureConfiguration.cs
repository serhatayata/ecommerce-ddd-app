using System.Reflection;
using Common.Domain.Repositories;
using Common.Infrastructure.Persistence;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Stock.Application.Consumers;
using Stock.Domain.Contracts;
using Stock.Infrastructure.Persistence;
using Stock.Infrastructure.Repositories.StockItems;

namespace Stock.Infrastructure;

public static class StockInfrastructureConfiguration
{
    public static IServiceCollection AddStockInfrastructure(
    this IServiceCollection services,
    IConfiguration configuration)
    {
        services
            .AddDatabase(configuration)
            .AddRepositories()
            .AddTransient<IDbInitializer, StockDbInitializer>()
            .AddTransient<IStockItemRepository, StockItemRepository>()
            .AddQueueConfigurations(configuration);

        return services;
    }

    private static IServiceCollection AddDatabase(
    this IServiceCollection services,
    IConfiguration configuration)
    {
        services
            .AddScoped<DbContext, StockDbContext>()
            .AddDbContext<StockDbContext>(options => options
                .UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    sqlOptions => sqlOptions
                        .MigrationsHistoryTable("__EFMigrationsHistory", "stock")
                        .EnableRetryOnFailure(
                            maxRetryCount: 10,
                            maxRetryDelay: TimeSpan.FromSeconds(30),
                            errorNumbersToAdd: null)
                        .MigrationsAssembly(
                            typeof(StockDbContext).Assembly.FullName)));

        return services;
    }

    private static IServiceCollection AddRepositories(
    this IServiceCollection services)
    {
        services
            .Scan(scan => scan
                .FromAssemblies(Assembly.GetExecutingAssembly())
                .AddClasses(classes => classes
                    .AssignableTo(typeof(IRepository<,>)))
                .AsImplementedInterfaces()
                .WithTransientLifetime());

        return services;
    }

    private static IServiceCollection AddQueueConfigurations(
    this IServiceCollection services,
    IConfiguration configuration)
    {
        return services.AddMassTransit(m =>
        {
            m.AddConsumer<StockAddRequestEventConsumer>();
            m.AddConsumer<StockItemCreateRequestEventConsumer>();
            m.AddConsumer<StockRemoveRequestEventConsumer>();
            m.AddConsumer<StockReserveRequestEventConsumer>();

            m.UsingRabbitMq((context, cfg) =>
            {
                var rabbitMQHost = configuration.GetConnectionString("RabbitMQ");
                cfg.Host(rabbitMQHost);

                cfg.ConfigureEndpoints(context);
            });
        });
    }
}