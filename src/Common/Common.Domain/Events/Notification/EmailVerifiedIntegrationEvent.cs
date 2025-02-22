namespace Common.Domain.Events.Notification;

public sealed record EmailVerifiedIntegrationEvent : IntegrationEvent
{
    public EmailVerifiedIntegrationEvent(
    Guid correlationId,
    int userId, 
    string email)
    : base(correlationId, DateTime.UtcNow)
    {
        UserId = userId;
        Email = email;
    }

    public int UserId { get; }
    public string Email { get; }
}