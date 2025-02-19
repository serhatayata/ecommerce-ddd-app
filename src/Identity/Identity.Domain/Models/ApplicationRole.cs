using Microsoft.AspNetCore.Identity;
using Common.Domain.Models;
using Identity.Domain.Events.Roles;

namespace Identity.Domain.Models;

public class ApplicationRole : IdentityRole<int>, IAggregateRoot
{
    private readonly List<IDomainEvent> _domainEvents = new();
    private readonly List<ApplicationRoleClaim> _claims = new();

    public ApplicationRole(string name) : base(name)
    {
        AddDomainEvent(new RoleCreatedDomainEvent(Id, name));
    }

    private ApplicationRole() { } // For EF Core

    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();
    public IReadOnlyCollection<ApplicationRoleClaim> Claims => _claims.AsReadOnly();

    // Domain methods with events
    public void UpdateName(string newName)
    {
        var oldName = Name;
        Name = newName;
        AddDomainEvent(new RoleNameUpdatedDomainEvent(Id, oldName, newName));
    }

    public void AddClaim(string type, string value)
    {
        var claim = new ApplicationRoleClaim 
        { 
            RoleId = Id,
            ClaimType = type,
            ClaimValue = value
        };
        
        _claims.Add(claim);
        AddDomainEvent(new RoleClaimAddedDomainEvent(Id, type, value));
    }

    public void RemoveClaim(string type, string value)
    {
        var claim = _claims.FirstOrDefault(c => 
            c.ClaimType == type && 
            c.ClaimValue == value);

        if (claim != null)
        {
            _claims.Remove(claim);
            AddDomainEvent(new RoleClaimRemovedDomainEvent(Id, type, value));
        }
    }

    private void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}