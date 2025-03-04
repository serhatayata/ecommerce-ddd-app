using Common.Domain.Events.Notification;
using MassTransit;

namespace Notification.Worker.Consumers;

public class SendVerificationEmailIntegrationEventConsumer : IConsumer<SendVerificationEmailIntegrationEvent>
{
    private readonly ILogger<SendVerificationEmailIntegrationEventConsumer> _logger;

    public SendVerificationEmailIntegrationEventConsumer(ILogger<SendVerificationEmailIntegrationEventConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<SendVerificationEmailIntegrationEvent> context)
    {
        var message = context.Message;
        _logger.LogInformation("Received verification email request for: {Email}", message.Email);

        // email sending...

        await Task.CompletedTask;
    }
}