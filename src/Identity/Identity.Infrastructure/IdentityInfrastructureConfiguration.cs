using System.Reflection;
using Common.Application.Extensions;
using Common.Domain.Models;
using Common.Domain.Repositories;
using Common.Infrastructure.Persistence;
using Identity.Application.Sagas.UserRegistration;
using Identity.Application.ServiceContracts;
using Identity.Domain.Events;
using Identity.Domain.Models;
using Identity.Infrastructure.Persistence;
using Identity.Infrastructure.Services;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Infrastructure;

public static class IdentityInfrastructureConfiguration
{
    public static IServiceCollection AddIdentityInfrastructure(
    this IServiceCollection services,
    IConfiguration configuration)
    {
        services
            .AddIdentity()
            .AddDatabase(configuration)
            .AddRepositories()
            .AddTransient<IDbInitializer, IdentityDbInitializer>()
            .AddSagaConfigurations(configuration);

        return services;
    }

    private static IServiceCollection AddIdentity(
        this IServiceCollection services)
    {
        services
            .AddTransient<IIdentityService, IdentityService>()
            .AddTransient<IJwtGenerator, JwtGeneratorService>()
            .AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = CommonModelConstants.Identity.MinPasswordLength;
            })
            .AddEntityFrameworkStores<IdentityDbContext>();

        return services;
    }

    private static IServiceCollection AddDatabase(
    this IServiceCollection services,
    IConfiguration configuration)
    {
        services
            .AddScoped<DbContext, IdentityDbContext>()
            .AddDbContext<IdentityDbContext>(options => options
                .UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    sqlOptions => sqlOptions
                        .EnableRetryOnFailure(
                            maxRetryCount: 10,
                            maxRetryDelay: TimeSpan.FromSeconds(30),
                            errorNumbersToAdd: null)
                        .MigrationsAssembly(
                            typeof(IdentityDbContext).Assembly.FullName)
                        .MigrationsHistoryTable("__EFMigrationsHistory", "identity")));

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
        services.AddDbContext<IdentitySagaDbContext>((srv, cfg) =>
        {
            cfg.UseSqlServer(connectionString: configuration.GetConnectionString("DefaultConnection"),
                             sqlServerOptionsAction: sqlOpt =>
                             {
                                 sqlOpt.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name);
                                 sqlOpt.MigrationsHistoryTable("__EFMigrationsHistory", "identitysaga");
                             });
        });

        return services.AddMassTransit(m =>
        {
            m.AddSagaStateMachine<UserRegistrationSaga, UserRegistrationState>()
             .EntityFrameworkRepository(opt =>
             {
                 opt.ExistingDbContext<IdentitySagaDbContext>();
             });

            // Other sagas
            // m.AddSagaStateMachine<PasswordResetSaga, PasswordResetState>()
            //   .EntityFrameworkRepository(opt =>
            //   {
            //     opt.ExistingDbContext<IdentitySagaDbContext>();
            //   });

            m.UsingRabbitMq((context, cfg) =>
            {
                var rabbitMQHost = configuration.GetConnectionString("RabbitMQ");
                cfg.Host(rabbitMQHost);
                
                #region UserCreatedDomainEvent
                var userCreatedDomainEventName = MessageBrokerExtensions.GetQueueName<UserCreatedDomainEvent>();
                cfg.ReceiveEndpoint(userCreatedDomainEventName, e =>
                {
                    e.ConfigureSaga<UserRegistrationState>(context);
                });
                #endregion
            });
        });
    }
}