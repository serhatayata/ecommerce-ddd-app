using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PaymentSystem.Application;
using PaymentSystem.Domain;
using PaymentSystem.Infrastructure;

namespace PaymentSystem.IoC;

public static class PaymentSystemModule
{
    public static void Register(
    this IServiceCollection services,
    IConfiguration configuration)
    {
        services
        .AddPaymentSystemApplication(configuration)
        .AddPaymentSystemInfrastructure(configuration)
        .AddPaymentSystemDomain();
    }
}