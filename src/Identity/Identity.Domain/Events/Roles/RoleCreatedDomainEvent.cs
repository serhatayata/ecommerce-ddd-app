using Common.Domain.Models;

namespace Identity.Domain.Events.Roles;

public record RoleCreatedDomainEvent(int RoleId, string Name) : IDomainEvent;