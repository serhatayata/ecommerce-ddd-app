namespace Common.Domain.Events.Identity;

public record UserCreatedEvent : IntegrationEvent
{
    public int UserId { get; }
    public string Email { get; }

    public UserCreatedEvent(
    Guid correlationId,
    int userId, 
    string email)
    : base(correlationId, DateTime.UtcNow)
    {
        UserId = userId;
        Email = email;
    }
}