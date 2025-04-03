using Common.Domain.Events.Shippings;
using MassTransit;
using Microsoft.Extensions.Logging;
using MediatR;
using Shipping.Application.Commands.Shipments.Deliver;

namespace Shipping.Application.Consumers;

public class DeliverShipmentIntegrationEventConsumer : IConsumer<DeliverShipmentIntegrationEvent>
{
    private readonly ILogger<DeliverShipmentIntegrationEventConsumer> _logger;
    private readonly IMediator _mediator;
    private readonly IPublishEndpoint _publishEndpoint;

    public DeliverShipmentIntegrationEventConsumer(
        ILogger<DeliverShipmentIntegrationEventConsumer> logger,
        IMediator mediator,
        IPublishEndpoint publishEndpoint)
    {
        _logger = logger;
        _mediator = mediator;
        _publishEndpoint = publishEndpoint;
    }

    public async Task Consume(ConsumeContext<DeliverShipmentIntegrationEvent> context)
    {
        var message = context.Message;
        _logger.LogInformation("Received shipment delivered request for: {TrackingNumber}", 
            message.TrackingNumber);

        try
        {
            var result = await _mediator.Send(new DeliverShipmentCommand(){ ShipmentId = message.ShipmentId });

            if (result.Succeeded)
                await _publishEndpoint.Publish(new ShipmentDeliveredIntegrationEvent(
                    message.CorrelationId,
                    message.ShipmentId,
                    message.TrackingNumber,
                    message.DeliveredDate));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing shipment delivery for tracking number: {TrackingNumber}", 
                message.TrackingNumber);
        }
    }
}