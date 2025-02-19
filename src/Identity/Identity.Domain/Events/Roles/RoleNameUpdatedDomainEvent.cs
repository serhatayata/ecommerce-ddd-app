using Common.Domain.Models;

namespace Identity.Domain.Events.Roles;

public record RoleNameUpdatedDomainEvent(int RoleId, string OldName, string NewName) : IDomainEvent;