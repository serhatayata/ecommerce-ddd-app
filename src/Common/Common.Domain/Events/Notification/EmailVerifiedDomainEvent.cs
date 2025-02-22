using Common.Domain.Models;

namespace Common.Domain.Events.Notification;

public class EmailVerifiedDomainEvent : IDomainEvent
{
    public EmailVerifiedDomainEvent(Guid userId, string email)
    {
        UserId = userId;
        Email = email;
    }

    public Guid UserId { get; }
    public string Email { get; }
    public Guid CorrelationId { get; }
}