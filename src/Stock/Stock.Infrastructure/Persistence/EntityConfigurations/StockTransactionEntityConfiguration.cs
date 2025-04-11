using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stock.Domain.Models.Stocks;

namespace Stock.Infrastructure.Persistence.EntityConfigurations;

public class StockTransactionEntityConfiguration : IEntityTypeConfiguration<StockTransaction>
{
    public void Configure(EntityTypeBuilder<StockTransaction> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Quantity)
            .IsRequired();

        builder.Property(x => x.Type)
            .IsRequired();

        builder.Property(x => x.Reason)
            .IsRequired();

        builder.Property(x => x.TransactionDate)
            .IsRequired();

        builder.HasIndex(x => x.TransactionDate);
    }
}
