using Identity.Application.Sagas.UserRegistration;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Persistence.Configurations;

public class UserRegistrationStateMap : SagaClassMap<UserRegistrationState>
{
    public void Configure(EntityTypeBuilder<UserRegistrationState> builder, ModelBuilder model)
    {
        builder.ToTable("UserRegistrationState", "dbo");

        builder.HasKey(x => x.CorrelationId);
        builder.Property(x => x.CorrelationId).ValueGeneratedNever();
        
        builder.Property(x => x.UserId);
        builder.Property(x => x.Email);
        builder.Property(x => x.CurrentState);
        builder.Property(x => x.CreatedAt);
        builder.Property(x => x.CompletedAt);
        builder.Property(x => x.FailureReason);
    }
}