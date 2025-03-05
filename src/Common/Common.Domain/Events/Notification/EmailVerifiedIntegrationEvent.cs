namespace Common.Domain.Events.Notification;

public sealed record EmailVerifiedIntegrationEvent : IntegrationEvent
{
    public EmailVerifiedIntegrationEvent(
    Guid correlationId,
    string email)
    : base(correlationId, DateTime.UtcNow)
    {
        Email = email;
    }

    public string Email { get; }
}