using Common.Domain.Events.Notification;
using MassTransit;

namespace Notification.Worker.Consumers;

public class SendVerificationEmailRequestEventConsumer : IConsumer<SendVerificationEmailRequestEvent>
{
    private readonly ILogger<SendVerificationEmailRequestEventConsumer> _logger;
    private readonly IPublishEndpoint _publishEndpoint;

    public SendVerificationEmailRequestEventConsumer(
        ILogger<SendVerificationEmailRequestEventConsumer> logger,
        IPublishEndpoint publishEndpoint)
    {
        _logger = logger;
        _publishEndpoint = publishEndpoint;
    }

    public async Task Consume(ConsumeContext<SendVerificationEmailRequestEvent> context)
    {
        var message = context.Message;
        _logger.LogInformation("Received verification email request for: {Email}", message.Email);

        // email sending...

        await _publishEndpoint.Publish(new EmailVerifiedEvent(message.CorrelationId, message.Email));
    }
}