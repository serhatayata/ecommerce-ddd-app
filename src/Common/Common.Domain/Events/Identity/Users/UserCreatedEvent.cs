using Common.Domain.Models;
using MassTransit;

namespace Common.Domain.Events.Identity.Users;

public record UserCreatedEvent : CorrelatedBy
{
    public int UserId { get; }
    public string Email { get; }
    public Guid CorrelationId { get; }

    public UserCreatedEvent(int userId, string email)
    {
        UserId = userId;
        Email = email;
    }
}