using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderManagement.Application;
using OrderManagement.Domain;
using OrderManagement.Infrastructure;

namespace OrderManagement.IoC;

public static class OrderManagementModule
{
    public static void Register(
    this IServiceCollection services,
    IConfiguration configuration)
    {
        services
        .AddOrderManagementApplication(configuration)
        .AddOrderManagementInfrastructure(configuration)
        .AddOrderManagementDomain();
    }
}