using Common.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaymentSystem.Domain.Models;

namespace PaymentSystem.Infrastructure.Persistence.EntityConfigurations;

public class PaymentInfoEntityConfiguration : IEntityTypeConfiguration<PaymentInfo>
{
    public void Configure(EntityTypeBuilder<PaymentInfo> builder)
    {
        builder.ToTable("PaymentInfo", "paymentsystem");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.Property(o => o.OrderId)
            .HasConversion(p => p.Value, p => OrderId.From(p))
            .IsRequired();
        
        builder.Property(x => x.CardNumber)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(x => x.HolderName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.ExpirationDate)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();
    }
}