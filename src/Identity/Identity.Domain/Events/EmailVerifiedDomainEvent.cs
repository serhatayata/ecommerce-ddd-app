using Common.Domain.Models;

namespace Identity.Domain.Events;

public sealed record EmailVerifiedDomainEvent : IDomainEvent
{
    public EmailVerifiedDomainEvent(
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