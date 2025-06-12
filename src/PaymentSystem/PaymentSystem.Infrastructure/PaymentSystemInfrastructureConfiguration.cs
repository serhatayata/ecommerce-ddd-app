using System.Reflection;
using Common.Domain.Repositories;
using Common.Infrastructure.Persistence;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PaymentSystem.Application.Services.OrderManagements;
using PaymentSystem.Domain.Contracts;
using PaymentSystem.Infrastructure.Persistence;
using PaymentSystem.Infrastructure.Repositories;
using PaymentSystem.Infrastructure.Services.OrderManagements;

namespace PaymentSystem.Infrastructure;

public static class PaymentSystemInfrastructureConfiguration
{
    public static IServiceCollection AddPaymentSystemInfrastructure(
    this IServiceCollection services,
    IConfiguration configuration)
    {
        services
            .AddDatabase(configuration)
            .AddRepositories()
            .AddTransient<IDbInitializer, PaymentSystemDbInitializer>()
            .AddTransient<IPaymentRepository, PaymentRepository>()
            .AddTransient<IOrderManagementApiService, OrderManagementApiService>()
            .AddQueueConfigurations(configuration);

        return services;
    }

    private static IServiceCollection AddDatabase(
    this IServiceCollection services,
    IConfiguration configuration)
    {
        services
            .AddScoped<DbContext, PaymentSystemDbContext>()
            .AddDbContext<PaymentSystemDbContext>(options => options
                .UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    sqlOptions => sqlOptions
                        .MigrationsHistoryTable("__EFMigrationsHistory", "paymentsystem")
                        .EnableRetryOnFailure(
                            maxRetryCount: 10,
                            maxRetryDelay: TimeSpan.FromSeconds(30),
                            errorNumbersToAdd: null)
                        .MigrationsAssembly(
                            typeof(PaymentSystemDbContext).Assembly.FullName)));

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
            m.UsingRabbitMq((context, cfg) =>
            {
                var rabbitMQHost = configuration.GetConnectionString("RabbitMQ");
                cfg.Host(rabbitMQHost);

                cfg.ConfigureEndpoints(context);
            });
        });
    }
}