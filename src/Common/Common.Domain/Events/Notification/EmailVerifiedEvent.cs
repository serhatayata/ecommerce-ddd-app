namespace Common.Domain.Events.Notification;

public sealed record EmailVerifiedEvent : IntegrationEvent
{
    public EmailVerifiedEvent(
    Guid correlationId,
    string email)
    : base(correlationId, DateTime.UtcNow)
    {
        Email = email;
    }

    public string Email { get; }
}