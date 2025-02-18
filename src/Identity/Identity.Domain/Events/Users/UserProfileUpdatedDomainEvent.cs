using Common.Domain.Models;

namespace Identity.Domain.Events.Users;

public record UserProfileUpdatedDomainEvent(int UserId, string FirstName, string LastName) : IDomainEvent
{
}