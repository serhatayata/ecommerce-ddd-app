using Identity.Application.Sagas.UserRegistration;
using MassTransit;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Persistence.Configurations;

public class UserRegistrationStateMap : SagaClassMap<UserRegistrationState>
{
    public void Configure(EntityTypeBuilder<UserRegistrationState> builder)
    {
        builder.HasKey(x => x.CorrelationId);
        builder.Property(x => x.UserId).IsRequired();
        builder.Property(x => x.Email).IsRequired();
        builder.Property(x => x.CurrentState).IsRequired();
        builder.Property(x => x.CreatedAt);
        builder.Property(x => x.CompletedAt);
        builder.Property(x => x.FailureReason);
    }
}