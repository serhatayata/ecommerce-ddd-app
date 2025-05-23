using Common.Domain.ValueObjects;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderManagement.Application.Sagas;

namespace OrderManagement.Infrastructure.Persistence.EntityConfigurations;

public class OrderAddStateMap : SagaClassMap<OrderAddState>
{
    public void Configure(EntityTypeBuilder<OrderAddState> builder, ModelBuilder model)
    {
        builder.ToTable("OrderAddState", "dbo");

        builder.HasKey(x => x.CorrelationId);
        builder.Property(x => x.CorrelationId).ValueGeneratedNever();
        
        builder.Property(x => x.CurrentState);
        builder.Property(x => x.OrderId).HasConversion(p => p.Value, p => OrderId.From(p));;
        builder.Property(x => x.UserId);
        builder.Property(x => x.OrderDate);
        builder.Property(x => x.CreatedAt);
        builder.Property(x => x.CompletedAt);
        builder.Property(x => x.FailureReason);
        builder.Property(x => x.CreatedAt).HasDefaultValueSql("getdate()");
    }
}