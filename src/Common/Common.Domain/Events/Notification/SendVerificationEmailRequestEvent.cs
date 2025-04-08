namespace Common.Domain.Events.Notification;

public sealed record SendVerificationEmailRequestEvent : IntegrationEvent
{
    public SendVerificationEmailRequestEvent(
    Guid correlationId,
    string email) 
    : base(correlationId, DateTime.UtcNow)
    {
        Email = email;
    }
    
    public string Email { get; set; }
}