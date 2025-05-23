using Common.Domain.Events;
using Common.Domain.Models;
using Identity.Domain.Events;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Identity.Domain.Models;

public class ApplicationUser : IdentityUser<int>, IAggregateRoot
{
    private readonly List<ApplicationUserClaim> _claims = new();
    private readonly List<ApplicationUserRole> _userRoles = new();
    private readonly List<ApplicationUserLogin> _logins = new();
    private readonly List<ApplicationUserToken> _tokens = new();
    private readonly List<IDomainEvent> _domainEvents = new();

    public string FirstName { get; private set; } = null!;
    public string LastName { get; private set; } = null!;
    public DateTime CreatedOnUtc { get; private set; }
    public DateTime? LastModifiedOnUtc { get; private set; }

    public IReadOnlyCollection<ApplicationUserClaim> Claims => _claims.AsReadOnly();
    public IReadOnlyCollection<ApplicationUserRole> UserRoles => _userRoles.AsReadOnly();
    public IReadOnlyCollection<ApplicationUserLogin> Logins => _logins.AsReadOnly();
    public IReadOnlyCollection<ApplicationUserToken> Tokens => _tokens.AsReadOnly();

    [NotMapped]
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    private ApplicationUser(
        string email,
        string userName,
        string firstName,
        string lastName)
        : base()
    {
        Email = email;
        UserName = userName;
        FirstName = firstName;
        LastName = lastName;
        CreatedOnUtc = DateTime.UtcNow;
    }

    public static ApplicationUser Create(
    string email,
    string userName,
    string firstName,
    string lastName)
        => new ApplicationUser(email, userName, firstName, lastName);

    private ApplicationUser() { }

    // Domain methods
    public void UpdateProfile(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
        LastModifiedOnUtc = DateTime.UtcNow;
    }

    public void AddClaim(string type, string value)
    {
        var claim = new ApplicationUserClaim 
        { 
            UserId = Id,
            ClaimType = type,
            ClaimValue = value
        };
        
        _claims.Add(claim);
    }

    public void AddToRole(ApplicationRole role)
    {
        if (_userRoles.Any(x => x.RoleId == role.Id))
        {
            return;
        }

        var userRole = new ApplicationUserRole
        {
            UserId = Id,
            RoleId = role.Id
        };

        _userRoles.Add(userRole);
    }

    public void RaiseUserCreatedDomainEvent()
        => _domainEvents.Add(new UserCreatedDomainEvent(Id, Email));

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}