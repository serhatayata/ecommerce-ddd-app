using Common.Domain.Models;

namespace Identity.Domain.Events.Users;

public record UserClaimAddedDomainEvent(int UserId, string ClaimType, string ClaimValue) : IDomainEvent
{
}