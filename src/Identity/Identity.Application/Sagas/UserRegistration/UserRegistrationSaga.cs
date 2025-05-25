using MassTransit;
using Common.Domain.Events.Notification;
using Common.Domain.Events.Identity;
using Common.Domain.ValueObjects;

namespace Identity.Application.Sagas.UserRegistration;

public class UserRegistrationSaga : MassTransitStateMachine<UserRegistrationState>
{
    public State EmailVerificationPending { get; private set; }
    public State Failed { get; private set; }
    public State Completed { get; private set; }

    //Integration Events
    public Event<UserCreatedEvent> UserCreatedEvent { get; private set; }
    public Event<EmailVerifiedEvent> EmailVerifiedEvent { get; private set; }

    public UserRegistrationSaga()
    {
        InstanceState(x => x.CurrentState);

        //Domain events
        Event(() => UserCreatedEvent, x => x.CorrelateById(context => context.Message.CorrelationId));

        //Integration event
        Event(() => EmailVerifiedEvent, x => x.CorrelateById(context => context.Message.CorrelationId));

        Initially(
            When(UserCreatedEvent)
                .Then(context =>
                {
                    context.Saga.UserId = context.Message.UserId;
                    context.Saga.Email = context.Message.Email;
                    context.Saga.CreatedAt = DateTime.UtcNow;
                })
                .TransitionTo(EmailVerificationPending)
                .Publish(context => {
                    return new SendVerificationEmailRequestEvent(
                    context.Saga.CorrelationId, 
                    context.Saga.Email);
                })
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