using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shipping.Domain.Models.Shipments;

namespace Shipping.Infrastructure.Persistence.EntityConfigurations;

public class ShipmentCompanyEntityConfigurations : IEntityTypeConfiguration<ShipmentCompany>
{
    public void Configure(EntityTypeBuilder<ShipmentCompany> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Code)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasMany(x => x.Shipments)
            .WithOne(s => s.ShipmentCompany)
            .HasForeignKey(s => s.ShipmentCompanyId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}