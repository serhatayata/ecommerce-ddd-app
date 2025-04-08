namespace Common.Domain.Events.Identity;

public record RegistrationCompletedEvent : IntegrationEvent
{
    public int UserId { get; }
    public string Email { get; }
    public DateTime CompletedAt { get; }

    public RegistrationCompletedEvent(
    Guid correlationId,
    int userId, 
    string email)
    : base (correlationId, DateTime.UtcNow)
    {
        UserId = userId;
        Email = email;
        CompletedAt = DateTime.UtcNow;
    }
}