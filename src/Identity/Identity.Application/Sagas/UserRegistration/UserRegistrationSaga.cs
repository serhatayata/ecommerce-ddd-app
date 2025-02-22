using MassTransit;
using Identity.Domain.Events;
using Common.Domain.Events.Notification;

namespace Identity.Application.Sagas.UserRegistration;

public class UserRegistrationSaga : MassTransitStateMachine<UserRegistrationState>
{
    public State AwaitingEmailVerification { get; private set; }
    public State EmailVerified { get; private set; }
    public State Completed { get; private set; }

    public Event<UserCreatedDomainEvent> UserCreatedDomainEvent { get; private set; }
    public Event<EmailVerifiedDomainEvent> EmailVerifiedDomainEvent { get; private set; }
    public Event<RegistrationCompletedDomainEvent> RegistrationCompletedDomainEvent { get; private set; }

    public UserRegistrationSaga()
    {
        InstanceState(x => x.CurrentState);

        Event(() => UserCreatedDomainEvent, x => x.CorrelateById(context => context.Message.CorrelationId));
        Event(() => EmailVerifiedDomainEvent, x => x.CorrelateById(context => context.Message.CorrelationId));
        Event(() => RegistrationCompletedDomainEvent, x => x.CorrelateById(context => context.Message.CorrelationId));

        Initially(
            When(UserCreatedDomainEvent)
                .Then(context =>
                {
                    context.Saga.UserId = context.Message.UserId;
                    context.Saga.Email = context.Message.Email;
                    context.Saga.CreatedAt = DateTime.UtcNow;
                })
                .TransitionTo(AwaitingEmailVerification)
                .Publish(context => new SendVerificationEmailIntegrationEvent(context.Saga.CorrelationId, context.Saga.Email))
        );

        During(AwaitingEmailVerification,
            When(EmailVerifiedDomainEvent)
                .TransitionTo(EmailVerified)
                .Publish(context => new SendWelcomeEmailIntegrationEvent(context.Saga.CorrelationId, context.Saga.Email))
        );

        During(EmailVerified,
            When(RegistrationCompletedDomainEvent)
                .Then(context => context.Saga.CompletedAt = DateTime.UtcNow)
                .TransitionTo(Completed)
        );
    }
}