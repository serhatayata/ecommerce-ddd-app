using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Shipping.Infrastructure.Persistence;

public class ShippingDbContextFactory : IDesignTimeDbContextFactory<ShippingDbContext>
{
    private readonly IMediator _mediator;

    public ShippingDbContextFactory(IMediator mediator)
    {
        _mediator = mediator;
    }

    public ShippingDbContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json",
                         optional: false,
                         reloadOnChange: true)
            .Build();

        var builder = new DbContextOptionsBuilder<ShippingDbContext>();
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        builder.UseSqlServer(
            connectionString,
            sqlOptions => sqlOptions
                .MigrationsHistoryTable("__EFMigrationsHistory", "shipping")
                .EnableRetryOnFailure(
                    maxRetryCount: 10,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null)
                .MigrationsAssembly(
                    typeof(ShippingDbContext).Assembly.FullName));

        return new ShippingDbContext(builder.Options, _mediator);    }
}