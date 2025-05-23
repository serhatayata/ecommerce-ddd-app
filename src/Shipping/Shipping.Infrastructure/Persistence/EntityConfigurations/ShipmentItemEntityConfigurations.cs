using Common.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shipping.Domain.Models.Shipments;

namespace Shipping.Infrastructure.Persistence.EntityConfigurations;

public class ShipmentItemEntityConfigurations : IEntityTypeConfiguration<ShipmentItem>
{
    public void Configure(EntityTypeBuilder<ShipmentItem> builder)
    {
        builder.ToTable("ShipmentItems");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.ProductId)
            .HasConversion(p => p.Value, p => ProductId.From(p))
            .IsRequired();

        builder.Property(x => x.ShipmentId)
            .HasConversion(p => p.Value, p => ShipmentId.From(p))
            .IsRequired();

        builder.Property(x => x.Quantity)
            .IsRequired();

        builder.HasOne(x => x.Shipment)
            .WithMany(s => s.Items)
            .HasForeignKey(x => x.ShipmentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}