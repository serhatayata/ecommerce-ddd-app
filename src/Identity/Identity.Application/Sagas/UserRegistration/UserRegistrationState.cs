using Common.Domain.ValueObjects;
using MassTransit;

namespace Identity.Application.Sagas.UserRegistration;

public class UserRegistrationState : SagaStateMachineInstance
{
    public Guid CorrelationId { get; set; }
    public string CurrentState { get; set; }
    public UserId UserId { get; set; }
    public string Email { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public string? FailureReason { get; set; }
}