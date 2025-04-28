using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaymentSystem.Domain.Models;

namespace PaymentSystem.Infrastructure.Persistence.EntityConfigurations;

public class PaymentEntityConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable("Payments", "paymentsystem");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.OwnsOne(x => x.Amount, amountBuilder =>
        {
            amountBuilder.Property(a => a.Amount)
                .HasColumnName("Amount")
                .IsRequired();
        });

        builder.Property(x => x.Method).IsRequired();

        builder.Property(x => x.Status).IsRequired();

        builder.Property(x => x.CreatedAt).IsRequired();

        builder.HasMany(x => x.Transactions)
            .WithOne(t => t.Payment)
            .HasForeignKey(t => t.PaymentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}