namespace Common.Domain.Events.Identity;

public record UserCreatedIntegrationEvent : IntegrationEvent
{
    public int UserId { get; }
    public string Email { get; }

    public UserCreatedIntegrationEvent(
    Guid correlationId,
    int userId, 
    string email)
    : base(correlationId, DateTime.UtcNow)
    {
        UserId = userId;
        Email = email;
    }
}