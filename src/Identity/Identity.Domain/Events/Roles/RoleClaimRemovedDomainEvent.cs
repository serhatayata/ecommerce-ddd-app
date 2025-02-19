using Common.Domain.Models;

namespace Identity.Domain.Events.Roles;

public record RoleClaimRemovedDomainEvent(int RoleId, string ClaimType, string ClaimValue) : IDomainEvent;