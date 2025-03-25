using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductCatalog.Domain.Models.Suppliers;

namespace ProductCatalog.Infrastructure.Persistence.EntityConfigurations;

public class SupplierEntityConfiguration : IEntityTypeConfiguration<Supplier>
{
    public void Configure(EntityTypeBuilder<Supplier> builder)
    {
        builder.ToTable("Suppliers");
        
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(s => s.ContactName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(s => s.Email)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(s => s.Phone)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(s => s.IsActive)
            .IsRequired();

        builder.OwnsOne(s => s.Address, a =>
        {
            a.Property(p => p.Street).IsRequired().HasMaxLength(200);
            a.Property(p => p.City).IsRequired().HasMaxLength(100);
            a.Property(p => p.State).IsRequired().HasMaxLength(50);
            a.Property(p => p.Country).IsRequired().HasMaxLength(50);
            a.Property(p => p.ZipCode).IsRequired().HasMaxLength(10);
        });

        builder.HasMany(s => s.Products)
            .WithOne(p => p.Supplier)
            .HasForeignKey("SupplierId")
            .OnDelete(DeleteBehavior.Restrict);
    }
}