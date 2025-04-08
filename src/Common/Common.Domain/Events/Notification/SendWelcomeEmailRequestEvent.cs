namespace Common.Domain.Events.Notification;

public sealed record SendWelcomeEmailRequestEvent : IntegrationEvent
{
    public SendWelcomeEmailRequestEvent(
    Guid correlationId, 
    string email) 
    : base(correlationId, DateTime.UtcNow)
    {
        Email = email;
    }

    public string Email { get; }
}