using Common.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaymentSystem.Domain.Models;

namespace PaymentSystem.Infrastructure.Persistence.EntityConfigurations;

public class PaymentTransactionEntityConfiguration : IEntityTypeConfiguration<PaymentTransaction>
{
    public void Configure(EntityTypeBuilder<PaymentTransaction> builder)
    {
        builder.HasKey(pt => pt.Id);

        builder.Property(pt => pt.Amount).IsRequired();


        builder.Property(pt => pt.Date).IsRequired();

        builder.Property(pt => pt.TransactionId).IsRequired();

        builder.HasOne(pt => pt.Payment)
            .WithMany(p => p.Transactions)
            .HasForeignKey(pt => pt.PaymentId)
            .IsRequired();
    }
}