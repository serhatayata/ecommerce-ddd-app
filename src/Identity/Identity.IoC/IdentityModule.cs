using Identity.Application;
using Identity.Domain;
using Identity.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.IoC;

public static class IdentityModule
{
    public static void Register(
    this IServiceCollection services,
    IConfiguration configuration)
    {
        services.AddIdentityApplication(configuration)
                .AddIdentityInfrastructure(configuration)
                .AddIdentityDomain();
    }
}