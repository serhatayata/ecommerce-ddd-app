using Common.Domain.Events;

namespace Identity.Domain.Events;

public sealed record UserCreatedDomainEvent : DomainEvent
{
    public UserCreatedDomainEvent(
    int userId, 
    string email)
    {
        UserId = userId;
        Email = email;
    }

    public int UserId { get; }
    public string Email { get; }
    public Guid CorrelationId { get; }
}