using Common.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using OrderManagement.Domain.Models.Orders;

namespace OrderManagement.Infrastructure.Persistence.EntityConfigurations;

public class OrderEntityConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders", "order");

        builder.HasKey(o => o.Id);

        builder.Property(o => o.Id)
            .ValueGeneratedOnAdd();

        builder.Property(o => o.UserId).HasConversion(p => p.Value, p => UserId.From(p)).IsRequired();

        builder.Property(o => o.OrderDate)
            .IsRequired();

        builder.Property(o => o.Status)
            .IsRequired()
            .HasConversion(
                v => v.Value,
                v => OrderStatus.FromValue<OrderStatus>(v)
            )
            .HasColumnType("int");

        builder.HasMany(o => o.OrderItems)
            .WithOne()
            .HasForeignKey("OrderId")
            .OnDelete(DeleteBehavior.Cascade);
    }
}