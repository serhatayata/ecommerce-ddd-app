using Common.Domain.Events;

namespace Identity.Domain.Events;

public sealed record UserNotCreatedDomainEvent : DomainEvent
{
    public UserNotCreatedDomainEvent(
    string email,
    string? reason,
    Guid correlationId) 
    : base(correlationId)
    {
        Email = email;
        Reason = reason;
    }

    public string Email { get; }
    public string? Reason { get; }
}