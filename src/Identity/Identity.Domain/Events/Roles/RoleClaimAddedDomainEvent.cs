using Common.Domain.Models;

namespace Identity.Domain.Events.Roles;

public record RoleClaimAddedDomainEvent(int RoleId, string ClaimType, string ClaimValue) : IDomainEvent;