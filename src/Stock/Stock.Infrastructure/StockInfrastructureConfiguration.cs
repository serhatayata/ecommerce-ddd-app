using System.Reflection;
using Common.Application.Extensions;
using Common.Domain.Events.Stocks;
using Common.Domain.Repositories;
using Common.Infrastructure.Persistence;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
            .AddQueueConfigurations();

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
                #region StockAddedEvent
                var stockAddedEventName = MessageBrokerExtensions.GetQueueName<StockAddedEvent>();
                cfg.ReceiveEndpoint(stockAddedEventName, e =>
                {
                    var exchangeName = MessageBrokerExtensions.GetExchangeName<StockAddedEvent>();
                    e.Bind(exchangeName, x =>
                    {
                        x.ExchangeType = "fanout";
                        x.Durable = true;
                    });
                });
                #endregion
                #region StockRemovedEvent
                var stockRemovedEventName = MessageBrokerExtensions.GetQueueName<StockRemovedEvent>();
                cfg.ReceiveEndpoint(stockRemovedEventName, e =>
                {
                    var exchangeName = MessageBrokerExtensions.GetExchangeName<StockRemovedEvent>();
                    e.Bind(exchangeName, x =>
                    {
                        x.ExchangeType = "fanout";
                        x.Durable = true;
                    });
                });
                #endregion
                #region StockReservedEvent
                var stockReservedEventName = MessageBrokerExtensions.GetQueueName<StockReservedEvent>();
                cfg.ReceiveEndpoint(stockReservedEventName, e =>
                {
                    var exchangeName = MessageBrokerExtensions.GetExchangeName<StockReservedEvent>();
                    e.Bind(exchangeName, x =>
                    {
                        x.ExchangeType = "fanout";
                        x.Durable = true;
                    });
                });
                #endregion
                #region StockItemCreatedEvent
                var stockItemCreatedEventName = MessageBrokerExtensions.GetQueueName<StockItemCreatedEvent>();
                cfg.ReceiveEndpoint(stockItemCreatedEventName, e =>
                {
                    var exchangeName = MessageBrokerExtensions.GetExchangeName<StockItemCreatedEvent>();
                    e.Bind(exchangeName, x =>
                    {
                        x.ExchangeType = "fanout";
                        x.Durable = true;
                    });
                });
                #endregion
            });
        });
    }
}