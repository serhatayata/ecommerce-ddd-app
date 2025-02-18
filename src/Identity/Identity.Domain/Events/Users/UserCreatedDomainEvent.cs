using Common.Domain.Models;

namespace Identity.Domain.Events.Users;

public record UserCreatedDomainEvent(int UserId, string Email) : IDomainEvent
{
}