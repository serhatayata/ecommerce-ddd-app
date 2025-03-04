using MassTransit;
using Identity.Domain.Events;
using Common.Domain.Events.Notification;

namespace Identity.Application.Sagas.UserRegistration;

public class UserRegistrationSaga : MassTransitStateMachine<UserRegistrationState>
{
    public State EmailVerificationPending { get; private set; }
    public State Failed { get; private set; }
     public State Completed { get; private set; }

    // Domain Events
    public Event<UserCreatedDomainEvent> UserCreatedDomainEvent { get; private set; }
    public Event<UserNotCreatedDomainEvent> UserNotCreatedDomainEvent { get; private set; }

    //Integration Events
    public Event<EmailVerifiedIntegrationEvent> EmailVerifiedEvent { get; private set; }

    public UserRegistrationSaga()
    {
        InstanceState(x => x.CurrentState);

        //Domain events
        Event(() => UserCreatedDomainEvent, x => x.CorrelateById(context => context.Message.CorrelationId));
        Event(() => UserNotCreatedDomainEvent, x => x.CorrelateById(context => context.Message.CorrelationId));

        //Integration event
        Event(() => EmailVerifiedEvent, x => x.CorrelateById(context => context.Message.CorrelationId));

        Initially(
            When(UserCreatedDomainEvent)
                .Then(context =>
                {
                    context.Saga.UserId = context.Message.UserId;
                    context.Saga.Email = context.Message.Email;
                    context.Saga.CreatedAt = DateTime.UtcNow;
                })
                .TransitionTo(EmailVerificationPending)
                .Publish(context => new SendVerificationEmailIntegrationEvent(
                    context.Saga.CorrelationId, 
                    context.Saga.Email)),
            
            When(UserNotCreatedDomainEvent)
                .Then(context =>
                {
                    context.Saga.Email = context.Message.Email;
                    context.Saga.FailureReason = context.Message.Reason;
                    context.Saga.CreatedAt = DateTime.UtcNow;
                })
                .TransitionTo(Failed)
        );

        During(EmailVerificationPending,
            When(EmailVerifiedEvent)
                .Then(context =>
                {
                    context.Saga.CompletedAt = DateTime.UtcNow;
                    context.Saga.Email = context.Message.Email;
                    context.Saga.CreatedAt = context.Message.CreationDate;
                })
                .TransitionTo(Completed));
    }
}