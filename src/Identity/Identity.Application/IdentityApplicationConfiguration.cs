using System.Reflection;
using Common.Application.Behaviours;
using Common.Application.Settings;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Application;

public static class IdentityApplicationConfiguration
{
    private static readonly Assembly Assembly = Assembly.GetExecutingAssembly();

    public static IServiceCollection AddIdentityApplication(
    this IServiceCollection services,
    IConfiguration configuration)
    {
        services
            .Configure<ApplicationSettings>(configuration.GetSection(nameof(ApplicationSettings)))
            .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly))
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

        return services;
    }
}