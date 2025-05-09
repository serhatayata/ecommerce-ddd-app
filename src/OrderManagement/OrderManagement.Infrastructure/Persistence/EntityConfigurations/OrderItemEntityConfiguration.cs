using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderManagement.Domain.Models.Orders;

namespace OrderManagement.Infrastructure.Persistence.EntityConfigurations;

public class OrderItemEntityConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("OrderItems");

        builder.HasKey(oi => oi.Id);

        builder.Property(oi => oi.OrderId)
            .IsRequired();

        builder.Property(oi => oi.ProductId)
            .IsRequired();

        builder.Property(oi => oi.Quantity)
            .IsRequired();

        builder.HasOne<Order>()
            .WithMany(o => o.OrderItems)
            .HasForeignKey(oi => oi.OrderId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}