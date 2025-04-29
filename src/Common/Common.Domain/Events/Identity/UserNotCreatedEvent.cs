namespace Common.Domain.Events.Identity;

public sealed record UserNotCreatedEvent : IntegrationEvent
{
    public string Email { get; }
    public string Reason { get; }

    public UserNotCreatedEvent(
    Guid correlationId,
    string? reason,
    string email)
    : base(correlationId, DateTime.UtcNow)
    {
        Email = email;
        Reason = reason;
    }
}