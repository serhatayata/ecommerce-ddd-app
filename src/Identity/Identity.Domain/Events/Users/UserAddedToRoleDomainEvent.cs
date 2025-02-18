using Common.Domain.Models;

namespace Identity.Domain.Events.Users;

public record UserAddedToRoleDomainEvent(int UserId, int RoleId) : IDomainEvent
{
}