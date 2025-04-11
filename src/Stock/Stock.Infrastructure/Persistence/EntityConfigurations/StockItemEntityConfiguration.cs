using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stock.Domain.Models.Stocks;

namespace Stock.Infrastructure.Persistence.EntityConfigurations;

public class StockItemEntityConfiguration : IEntityTypeConfiguration<StockItem>
{
    public void Configure(EntityTypeBuilder<StockItem> builder)
    {
        builder.ToTable("StockItems", "stock");
        
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();
        
        builder.Property(x => x.ProductId)
            .IsRequired();

        builder.Property(x => x.Quantity)
            .IsRequired();

        builder.Property(x => x.Status)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(x => x.LastUpdated)
            .IsRequired();

        // Configure Location value object
        builder.OwnsOne(x => x.Location, locationBuilder =>
        {
            locationBuilder.Property(l => l.Warehouse)
                .HasColumnName("Location_Warehouse")
                .HasMaxLength(100)
                .IsRequired();

            locationBuilder.Property(l => l.Aisle)
                .HasColumnName("Location_Aisle")
                .HasMaxLength(50)
                .IsRequired();

            locationBuilder.Property(l => l.Shelf)
                .HasColumnName("Location_Shelf")
                .HasMaxLength(50)
                .IsRequired();
        });

        // Configure relationships
        builder.HasMany(x => x.Transactions)
            .WithOne(t => t.StockItem)
            .HasForeignKey(t => t.StockItemId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Reservations)
            .WithOne(r => r.StockItem)
            .HasForeignKey(r => r.StockItemId)
            .OnDelete(DeleteBehavior.Cascade);

        // Add useful indexes
        builder.HasIndex(x => x.ProductId);
    }
}