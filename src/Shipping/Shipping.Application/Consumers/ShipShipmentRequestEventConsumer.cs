using Common.Domain.Events.Shippings;
using MassTransit;
using Microsoft.Extensions.Logging;
using MediatR;
using Shipping.Application.Commands.Shipments.Ship;

namespace Shipping.Application.Consumers;

public class ShipShipmentRequestEventConsumer : IConsumer<ShipShipmentRequestEvent>
{
    private readonly ILogger<ShipShipmentRequestEventConsumer> _logger;
    private readonly IMediator _mediator;
    private readonly IPublishEndpoint _publishEndpoint;

    public ShipShipmentRequestEventConsumer(
        ILogger<ShipShipmentRequestEventConsumer> logger,
        IMediator mediator,
        IPublishEndpoint publishEndpoint)
    {
        _logger = logger;
        _mediator = mediator;
        _publishEndpoint = publishEndpoint;
    }

    public async Task Consume(ConsumeContext<ShipShipmentRequestEvent> context)
    {
        var message = context.Message;
        _logger.LogInformation("Received shipment shipped request for: {TrackingNumber}", 
            message.TrackingNumber);

        try
        {
            var result = await _mediator.Send(new ShipShipmentCommand() { ShipmentId = message.ShipmentId });

            if (result.Succeeded)
                await _publishEndpoint.Publish(new ShipmentShippedEvent(
                    message.CorrelationId,
                    message.ShipmentId,
                    message.TrackingNumber,
                    message.ShippedDate));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing shipment for tracking number: {TrackingNumber}", 
                message.TrackingNumber);
        }
    }
}