using Common.Domain.Events;
using System.Text.Json.Serialization;

namespace Identity.Domain.Events;

public sealed record UserCreatedDomainEvent : DomainEvent
{
    [JsonConstructor]
    public UserCreatedDomainEvent()
    {
    }
    
    public UserCreatedDomainEvent(
    int userId, 
    string email,
    Guid? correlationId = null) 
    : base(correlationId)
    {
        UserId = userId;
        Email = email;
    }

    public int UserId { get; init; }
    public string Email { get; init; }
}