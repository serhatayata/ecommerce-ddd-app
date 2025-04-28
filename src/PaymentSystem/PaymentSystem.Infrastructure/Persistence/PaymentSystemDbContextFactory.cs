using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace PaymentSystem.Infrastructure.Persistence;

public class PaymentSystemDbContextFactory : IDesignTimeDbContextFactory<PaymentSystemDbContext>
{
    public PaymentSystemDbContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json",
                         optional: false,
                         reloadOnChange: true)
            .Build();

        var builder = new DbContextOptionsBuilder<PaymentSystemDbContext>();
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        builder.UseSqlServer(
            connectionString,
            sqlOptions => sqlOptions
                .MigrationsHistoryTable("__EFMigrationsHistory", "paymentsystem")
                .EnableRetryOnFailure(
                    maxRetryCount: 10,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null)
                .MigrationsAssembly(
                    typeof(PaymentSystemDbContext).Assembly.FullName));

        return new PaymentSystemDbContext(builder.Options, null);    
    }
}