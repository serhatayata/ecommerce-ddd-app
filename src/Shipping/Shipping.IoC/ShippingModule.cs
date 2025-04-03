using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shipping.Application;
using Shipping.Infrastructure;
using Shipping.Domain;

namespace Shipping.IoC;

public static class ShippingModule
{
    public static void Register(
    this IServiceCollection services,
    IConfiguration configuration)
    {
        services
        .AddShippingApplication(configuration)
        .AddShippingInfrastructure(configuration)
        .AddShippingDomain();
    }
}