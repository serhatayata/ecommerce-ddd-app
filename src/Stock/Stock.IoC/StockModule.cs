using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Stock.Application;
using Stock.Infrastructure;
using Stock.Domain;

namespace Stock.IoC;

public static class StockModule
{
    public static void Register(
    this IServiceCollection services,
    IConfiguration configuration)
    {
        services
        .AddStockApplication(configuration)
        .AddStockInfrastructure(configuration)
        .AddStockDomain();
    }
}