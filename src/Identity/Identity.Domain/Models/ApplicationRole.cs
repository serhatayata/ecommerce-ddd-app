using Microsoft.AspNetCore.Identity;
using Common.Domain.Models;

namespace Identity.Domain.Models;

public class ApplicationRole : IdentityRole<int>, IAggregateRoot
{
    private readonly List<ApplicationRoleClaim> _claims = new();

    public ApplicationRole(string name) : base(name)
    {
    }

    private ApplicationRole() { } // For EF Core

    public IReadOnlyCollection<ApplicationRoleClaim> Claims => _claims.AsReadOnly();

    public void UpdateName(string newName)
    {
        var oldName = Name;
        Name = newName;
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
    }

    public void RemoveClaim(string type, string value)
    {
        var claim = _claims.FirstOrDefault(c => 
            c.ClaimType == type && 
            c.ClaimValue == value);

        if (claim != null)
            _claims.Remove(claim);
    }
}