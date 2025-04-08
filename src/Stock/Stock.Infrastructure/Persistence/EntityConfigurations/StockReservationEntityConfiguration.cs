using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stock.Domain.Models.Stocks;

namespace Stock.Infrastructure.Persistence.EntityConfigurations;

public class StockReservationEntityConfiguration : IEntityTypeConfiguration<StockReservation>
{
    public void Configure(EntityTypeBuilder<StockReservation> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.OrderId)
            .IsRequired();

        builder.Property(x => x.Quantity)
            .IsRequired();

        builder.Property(x => x.Status)
            .IsRequired();

        builder.Property(x => x.ReservationDate)
            .IsRequired();

        builder.HasIndex(x => x.OrderId);
        builder.HasIndex(x => x.ReservationDate);
    }
}
