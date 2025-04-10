using System.Reflection;
using Common.Application.Extensions;
using Common.Domain.Events.Shippings;
using Common.Domain.Repositories;
using Common.Infrastructure.Persistence;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shipping.Application.Consumers;
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
            .AddTransient<IShipmentRepository, ShipmentRepository>()
            .AddQueueConfigurations();

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

    private static IServiceCollection AddQueueConfigurations(
    this IServiceCollection services)
    {
        return services.AddMassTransit(m =>
        {
            m.AddConsumer<ShipShipmentRequestEventConsumer>();
            m.AddConsumer<DeliverShipmentRequestEventConsumer>();

            m.UsingRabbitMq((context, cfg) =>
            {
                #region ShipShipmentIntegrationEvent
                var shipShipmentIntegrationEventName = MessageBrokerExtensions.GetQueueName<ShipShipmentRequestEvent>();
                cfg.ReceiveEndpoint(shipShipmentIntegrationEventName, e =>
                {
                    e.ConfigureConsumer<ShipShipmentRequestEventConsumer>(context);
                    var exchangeName = MessageBrokerExtensions.GetExchangeName<ShipShipmentRequestEvent>();
                    e.Bind(exchangeName, x =>
                    {
                        x.ExchangeType = "fanout";
                        x.Durable = true;
                    });
                });
                #endregion
                #region DeliverShipmentIntegrationEvent
                var deliverShipmentIntegrationEventName = MessageBrokerExtensions.GetQueueName<DeliverShipmentRequestEvent>();
                cfg.ReceiveEndpoint(deliverShipmentIntegrationEventName, e =>
                {
                    e.ConfigureConsumer<DeliverShipmentRequestEventConsumer>(context);
                    var exchangeName = MessageBrokerExtensions.GetExchangeName<DeliverShipmentRequestEvent>();
                    e.Bind(exchangeName, x =>
                    {
                        x.ExchangeType = "fanout";
                        x.Durable = true;
                    });
                });
                #endregion
                #region ShipmentShippedIntegrationEvent
                var shipmentShippedIntegrationEventName = MessageBrokerExtensions.GetQueueName<ShipmentShippedEvent>();
                cfg.ReceiveEndpoint(shipmentShippedIntegrationEventName, e =>
                {
                    var exchangeName = MessageBrokerExtensions.GetExchangeName<ShipmentShippedEvent>();
                    e.Bind(exchangeName, x =>
                    {
                        x.ExchangeType = "fanout";
                        x.Durable = true;
                    });
                });
                #endregion
                #region ShipmentDeliveredIntegrationEvent
                var shipmentDeliveredIntegrationEventName = MessageBrokerExtensions.GetQueueName<ShipmentDeliveredEvent>();
                cfg.ReceiveEndpoint(shipmentDeliveredIntegrationEventName, e =>
                {
                    var exchangeName = MessageBrokerExtensions.GetExchangeName<ShipmentDeliveredEvent>();
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