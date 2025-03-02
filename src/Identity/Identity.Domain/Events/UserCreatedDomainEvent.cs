using Common.Domain.Events;

namespace Identity.Domain.Events;

public sealed record UserCreatedDomainEvent : DomainEvent
{
    public UserCreatedDomainEvent(
    int userId, 
    string email,
    Guid correlationId) 
    : base(correlationId)
    {
        UserId = userId;
        Email = email;
        CorrelationId = correlationId;
    }

    public int UserId { get; }
    public string Email { get; }
}