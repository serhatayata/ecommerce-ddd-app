using Microsoft.AspNetCore.Identity;
using Common.Domain.Models;

namespace Identity.Domain.Models;

public class ApplicationRole : IdentityRole<int>, IAggregateRoot
{
    private readonly List<IDomainEvent> _domainEvents = new();

    public ApplicationRole(string name) : base(name)
    {
    }

    private ApplicationRole() { } // For EF Core

    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();
}