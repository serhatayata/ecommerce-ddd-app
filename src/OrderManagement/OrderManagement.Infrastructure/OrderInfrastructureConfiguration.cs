using System.Reflection;
using Common.Domain.Repositories;
using Common.Infrastructure.Persistence;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderManagement.Application.Sagas;
using OrderManagement.Application.Services.PaymentSystems;
using OrderManagement.Application.Services.Products;
using OrderManagement.Domain.Contracts;
using OrderManagement.Infrastructure.Persistence;
using OrderManagement.Infrastructure.Repositories;
using OrderManagement.Infrastructure.Services.PaymentSystems;
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
            .AddTransient<IDbInitializer, OrderManagementSagaDbInitializer>()
            .AddTransient<IOrderRepository, OrderRepository>()
            .AddTransient<IProductCatalogApiService, ProductCatalogApiService>()
            .AddTransient<IPaymentSystemApiService, PaymentSystemApiService>()
            .AddSagaConfigurations(configuration);

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
                    .AssignableTo(typeof(IRepository<,>)))
                .AsImplementedInterfaces()
                .WithTransientLifetime());

        return services;
    }

    private static IServiceCollection AddSagaConfigurations(
    this IServiceCollection services,
    IConfiguration configuration)
    {
       services.AddDbContext<OrderManagementSagaDbContext>((srv, cfg) =>
        {
            cfg.UseSqlServer(connectionString: configuration.GetConnectionString("DefaultConnection"),
                             sqlServerOptionsAction: sqlOpt =>
                             {
                                 sqlOpt.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name);
                                 sqlOpt.MigrationsHistoryTable("__EFMigrationsHistory", "ordermanagementsaga");
                             });
        });

        return services.AddMassTransit(m =>
        {
            m.AddSagaStateMachine<OrderAddSaga, OrderAddState>()
             .EntityFrameworkRepository(opt =>
             {
                 opt.ExistingDbContext<OrderManagementSagaDbContext>();
             });
 
            m.UsingRabbitMq((context, cfg) =>
            {
                var rabbitMQHost = configuration.GetConnectionString("RabbitMQ");
                cfg.Host(rabbitMQHost);

                cfg.ConfigureEndpoints(context);
            });
        });
    }
}