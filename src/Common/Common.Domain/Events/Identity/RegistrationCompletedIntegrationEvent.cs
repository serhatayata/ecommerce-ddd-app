namespace Common.Domain.Events.Identity;

public record RegistrationIntegrationEvent : IntegrationEvent
{
    public int UserId { get; }
    public string Email { get; }
    public DateTime CompletedAt { get; }

    public RegistrationIntegrationEvent(
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