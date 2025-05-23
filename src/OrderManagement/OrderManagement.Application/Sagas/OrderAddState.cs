using System;
using Common.Domain.ValueObjects;
using MassTransit;

namespace OrderManagement.Application.Sagas;

public class OrderAddState : SagaStateMachineInstance
{
    public Guid CorrelationId { get; set; }
    public string CurrentState { get; set; }
    public OrderId OrderId { get; set; }
    public UserId UserId { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public string? FailureReason { get; set; }
}