using Common.Domain.Models;
using Identity.Domain.Events.Users;
using Microsoft.AspNetCore.Identity;

namespace Identity.Domain.Models;

public class ApplicationUser : IdentityUser<int>, IAggregateRoot
{
    private readonly List<IDomainEvent> _domainEvents = new();
    private readonly List<ApplicationUserClaim> _claims = new();
    private readonly List<ApplicationUserRole> _userRoles = new();
    private readonly List<ApplicationUserLogin> _logins = new();
    private readonly List<ApplicationUserToken> _tokens = new();

    public string FirstName { get; private set; } = null!;
    public string LastName { get; private set; } = null!;
    public DateTime CreatedOnUtc { get; private set; }
    public DateTime? LastModifiedOnUtc { get; private set; }

    public IReadOnlyCollection<ApplicationUserClaim> Claims => _claims.AsReadOnly();
    public IReadOnlyCollection<ApplicationUserRole> UserRoles => _userRoles.AsReadOnly();
    public IReadOnlyCollection<ApplicationUserLogin> Logins => _logins.AsReadOnly();
    public IReadOnlyCollection<ApplicationUserToken> Tokens => _tokens.AsReadOnly();
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    // Constructor for creating new users
    public ApplicationUser(
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
        
        AddDomainEvent(new UserCreatedDomainEvent(Id, Email));
    }

    public ApplicationUser(
        string email)
        : base()
    {
        Email = email;
        CreatedOnUtc = DateTime.UtcNow;
        
        AddDomainEvent(new UserCreatedDomainEvent(Id, Email));
    }

    // Private constructor for EF Core
    private ApplicationUser() { }

    // Domain methods
    public void UpdateProfile(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
        LastModifiedOnUtc = DateTime.UtcNow;
        
        AddDomainEvent(new UserProfileUpdatedDomainEvent(Id, FirstName, LastName));
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
        AddDomainEvent(new UserClaimAddedDomainEvent(Id, type, value));
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
        AddDomainEvent(new UserAddedToRoleDomainEvent(Id, role.Id));
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