namespace Identity.Domain.Events;

public sealed record RegistrationCompletedDomainEvent
{
    public int UserId { get; }
    public string Email { get; }
    public DateTime CompletedAt { get; }
    public Guid CorrelationId { get; }

    public RegistrationCompletedDomainEvent(
    int userId, 
    string email)
    {
        UserId = userId;
        Email = email;
        CompletedAt = DateTime.UtcNow;
    }
}