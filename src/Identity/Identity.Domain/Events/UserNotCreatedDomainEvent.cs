using System.Text.Json.Serialization;
using Common.Domain.Events;

namespace Identity.Domain.Events;

public sealed record UserNotCreatedDomainEvent : DomainEvent
{
    [JsonConstructor]
    public UserNotCreatedDomainEvent()
    {
    }

    public UserNotCreatedDomainEvent(
    string email,
    string? reason,
    Guid? correlationId = null) 
    : base(correlationId)
    {
        Email = email;
        Reason = reason;
    }

    public string Email { get; init; }
    public string? Reason { get; init; }
}