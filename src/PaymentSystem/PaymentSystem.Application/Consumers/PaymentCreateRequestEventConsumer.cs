using Common.Domain.Events.PaymentSystems;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using PaymentSystem.Application.Commands.Payments;

namespace PaymentSystem.Application.Consumers;

public class PaymentCreateRequestEventConsumer : IConsumer<PaymentCreateRequestEvent>
{
    private readonly ILogger<PaymentCreateRequestEventConsumer> _logger;
    private readonly IMediator _mediator;

    public PaymentCreateRequestEventConsumer(
        ILogger<PaymentCreateRequestEventConsumer> logger, 
        IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }
    
    public async Task Consume(
    ConsumeContext<PaymentCreateRequestEvent> context)
    {
            var message = context.Message;
        _logger.LogInformation("Received payment create request for: {OrderId}", message.OrderId);

        try
        {
            _ = await _mediator.Send(new PaymentCreateCommand() 
            {
                OrderId = message.OrderId,
                CorrelationId = message.CorrelationId
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing payment create request for item ID: {OrderId}", message.OrderId);
        }
    }
}