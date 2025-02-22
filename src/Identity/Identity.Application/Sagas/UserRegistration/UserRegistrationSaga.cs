using MassTransit;
using Common.Domain.Events.Identity.Users;
using Common.Domain.Events.Notification;

namespace Identity.Application.Sagas.UserRegistration;

public class UserRegistrationSaga : MassTransitStateMachine<UserRegistrationState>
{
    public State AwaitingEmailVerification { get; private set; }
    public State EmailVerified { get; private set; }
    public State Completed { get; private set; }

    public Event<UserCreatedDomainEvent> UserCreatedEvent { get; private set; }
    public Event<EmailVerifiedDomainEvent> EmailVerifiedEvent { get; private set; }
    public Event<RegistrationCompletedEvent> RegistrationCompletedEvent { get; private set; }

    public UserRegistrationSaga()
    {
        InstanceState(x => x.CurrentState);

        Event(() => UserCreatedEvent, x => x.CorrelateById(context => context.Message.CorrelationId));
        Event(() => EmailVerifiedEvent, x => x.CorrelateById(context => context.Message.CorrelationId));
        Event(() => RegistrationCompletedEvent, x => x.CorrelateById(context => context.Message.CorrelationId));

        Initially(
            When(UserCreatedEvent)
                .Then(context =>
                {
                    context.Saga.UserId = context.Message.UserId;
                    context.Saga.Email = context.Message.Email;
                    context.Saga.CreatedAt = DateTime.UtcNow;
                })
                .TransitionTo(AwaitingEmailVerification)
                .Publish(context => new SendVerificationEmailEvent(context.Saga.CorrelationId, context.Saga.Email))
        );

        During(AwaitingEmailVerification,
            When(EmailVerifiedEvent)
                .TransitionTo(EmailVerified)
                .Publish(context => new SendWelcomeEmailEvent(context.Saga.CorrelationId, context.Saga.Email))
        );

        During(EmailVerified,
            When(RegistrationCompletedEvent)
                .Then(context => context.Saga.CompletedAt = DateTime.UtcNow)
                .TransitionTo(Completed)
        );
    }
}