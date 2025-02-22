namespace Common.Domain.Events.Notification;

public sealed record SendVerificationEmailIntegrationEvent : IntegrationEvent
{
    public SendVerificationEmailIntegrationEvent(
    Guid correlationId,
    string email) 
    : base(correlationId, DateTime.UtcNow)
    {
        Email = email;
    }
    
    public string Email { get; set; }
}