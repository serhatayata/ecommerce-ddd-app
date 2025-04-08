using System.Reflection;
using Common.Application.Behaviours;
using Common.Application.Mapping;
using Common.Application.Settings;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Stock.Application;

public static class StockApplicationConfiguration
{
    private static readonly Assembly Assembly = Assembly.GetExecutingAssembly();

    public static IServiceCollection AddStockApplication(
    this IServiceCollection services,
    IConfiguration configuration)
    {
        var applicationSettings = configuration.GetSection(nameof(ApplicationSettings));

        services
            .Configure<ApplicationSettings>(applicationSettings)
            .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly))
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>))
            .AddAutoMapper(cfg => cfg.AddProfile(new MappingProfile(Assembly)));

        return services;
    }
}