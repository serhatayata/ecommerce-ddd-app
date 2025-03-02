using MassTransit;
using Identity.Domain.Events;
using Common.Domain.Events.Notification;

namespace Identity.Application.Sagas.UserRegistration;

public class UserRegistrationSaga : MassTransitStateMachine<UserRegistrationState>
{
    public State EmailVerificationPending { get; private set; }
    public State Failed { get; private set; }
     // public State Completed { get; private set; }

    public Event<UserCreatedDomainEvent> UserCreatedDomainEvent { get; private set; }
    public Event<UserNotCreatedDomainEvent> UserNotCreatedDomainEvent { get; private set; }

    public UserRegistrationSaga()
    {
        InstanceState(x => x.CurrentState);

        Event(() => UserCreatedDomainEvent, x => x.CorrelateById(context => context.Message.CorrelationId));
        Event(() => UserNotCreatedDomainEvent, x => x.CorrelateById(context => context.Message.CorrelationId));

        Initially(
            When(UserCreatedDomainEvent)
                .Then(context =>
                {
                    context.Saga.UserId = context.Message.UserId;
                    context.Saga.Email = context.Message.Email;
                    context.Saga.CreatedAt = DateTime.UtcNow;
                })
                .TransitionTo(EmailVerificationPending)
                .Publish(context => new SendVerificationEmailIntegrationEvent(context.Saga.CorrelationId, context.Saga.Email)),
            
            When(UserNotCreatedDomainEvent)
                .Then(context =>
                {
                    context.Saga.Email = context.Message.Email;
                    context.Saga.FailureReason = context.Message.Reason;
                    context.Saga.CreatedAt = DateTime.UtcNow;
                })
                .TransitionTo(Failed)
        );
    }
}