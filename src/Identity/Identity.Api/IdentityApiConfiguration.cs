using Common.Api.Services;
using Common.Application.Contracts;
using FluentValidation;
using Identity.Application;

namespace Identity.Api;

public static class IdentityApiConfiguration
{
    public static IServiceCollection AddIdentityApi(
    this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining(typeof(IdentityApplicationConfiguration))
                .AddScoped<ICurrentUser, CurrentUserService>();

        return services;
    }
}