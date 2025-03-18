using System.Reflection;
using Common.Domain.Repositories;
using Common.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Order.Infrastructure.Persistence;

namespace Order.Infrastructure;

public static class OrderInfrastructureConfiguration
{
    public static IServiceCollection AddOrderInfrastructure(
    this IServiceCollection services,
    IConfiguration configuration)
    {
        services
            .AddDatabase(configuration)
            .AddRepositories()
            .AddTransient<IDbInitializer, OrderDbInitializer>()
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
                    .AssignableTo(typeof(IRepository<>)))
                .AsImplementedInterfaces()
                .WithTransientLifetime());

        return services;
    }

    private static IServiceCollection AddSagaConfigurations(
    this IServiceCollection services,
    IConfiguration configuration)
    {
        services.AddDbContext<OrderSagaDbContext>((srv, cfg) =>
        {
            cfg.UseSqlServer(connectionString: configuration.GetConnectionString("DefaultConnection"),
                             sqlServerOptionsAction: sqlOpt =>
                             {
                                 sqlOpt.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name);
                                 sqlOpt.MigrationsHistoryTable("__EFMigrationsHistory", "ordersaga");
                             });
        });

        return services;
    }
}