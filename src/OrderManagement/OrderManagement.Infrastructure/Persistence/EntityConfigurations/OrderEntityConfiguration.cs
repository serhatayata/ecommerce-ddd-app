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
            .HasColumnName("OrderId")
            .ValueGeneratedNever();

        builder.Property(o => o.UserId)
            .IsRequired();

        builder.Property(o => o.OrderDate)
            .IsRequired();

        builder.Property(o => o.Status)
            .IsRequired()
            .HasColumnType("int");

        builder.HasMany(o => o.OrderItems)
            .WithOne()
            .HasForeignKey("OrderId")
            .OnDelete(DeleteBehavior.Cascade);
    }
}