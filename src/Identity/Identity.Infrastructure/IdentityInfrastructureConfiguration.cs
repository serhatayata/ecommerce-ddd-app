using System.Reflection;
using Common.Domain.Models;
using Common.Domain.Repositories;
using Common.Infrastructure.Persistence;
using Identity.Application.ServiceContracts;
using Identity.Domain.Models;
using Identity.Infrastructure.Persistence;
using Identity.Infrastructure.Services;
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
            .AddTransient<IDbInitializer, IdentityDbInitializer>();

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
                            typeof(IdentityDbContext).Assembly.FullName)));

        return services;
    }

    internal static IServiceCollection AddRepositories(
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