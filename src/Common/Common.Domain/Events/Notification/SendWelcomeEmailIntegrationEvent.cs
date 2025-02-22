namespace Common.Domain.Events.Notification;

public sealed record SendWelcomeEmailIntegrationEvent : IntegrationEvent
{
    public SendWelcomeEmailIntegrationEvent(
    Guid correlationId, 
    string email) 
    : base(correlationId, DateTime.UtcNow)
    {
        Email = email;
    }

    public string Email { get; }
}