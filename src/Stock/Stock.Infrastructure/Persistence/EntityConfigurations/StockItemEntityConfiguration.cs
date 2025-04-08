using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stock.Domain.Models.Stocks;

namespace Stock.Infrastructure.Persistence.EntityConfigurations;

public class StockItemEntityConfiguration : IEntityTypeConfiguration<StockItem>
{
    public void Configure(EntityTypeBuilder<StockItem> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.ProductId)
            .IsRequired();

        builder.Property(x => x.Quantity)
            .IsRequired();

        builder.Property(x => x.Status)
            .IsRequired();

        builder.Property(x => x.LastUpdated)
            .IsRequired();

        builder.OwnsOne(x => x.Location, locationBuilder =>
        {
            locationBuilder.Property(l => l.Warehouse).IsRequired();
            locationBuilder.Property(l => l.Aisle).IsRequired();
            locationBuilder.Property(l => l.Shelf).IsRequired();
            locationBuilder.Property(l => l.Bin).IsRequired();
        });

        builder.HasMany(x => x.Transactions)
            .WithOne(t => t.StockItem)
            .HasForeignKey(t => t.StockItemId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Reservations)
            .WithOne(r => r.StockItem)
            .HasForeignKey(r => r.StockItemId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.ProductId);
        builder.HasIndex("Location_Warehouse");
    }
}