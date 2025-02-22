using MassTransit;

namespace Common.Domain.Events.Identity.Users;

public record RegistrationCompletedEvent : CorrelatedBy<Guid>
{
    public int UserId { get; }
    public string Email { get; }
    public DateTime CompletedAt { get; }
    public Guid CorrelationId { get; }

    public RegistrationCompletedEvent(
    int userId, 
    string email)
    {
        UserId = userId;
        Email = email;
        CompletedAt = DateTime.UtcNow;
    }
}