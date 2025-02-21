using Common.Infrastructure.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Infrastructure.Extensions;

public static class DbInitializerExtension
{
    public static IApplicationBuilder InitializeDB(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();

        var initializers = serviceScope.ServiceProvider.GetServices<IDbInitializer>();

        foreach (var initializer in initializers)
            initializer.Initialize();

        return app;
    }
}