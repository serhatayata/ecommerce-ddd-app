using Common.Domain.Events.Shippings;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Shipping.Application.Consumers;

public class ShipmentShippedIntegrationEventConsumer : IConsumer<ShipmentShippedIntegrationEvent>
{
    private readonly ILogger<ShipmentShippedIntegrationEventConsumer> _logger;
    private readonly IPublishEndpoint _publishEndpoint;

    public ShipmentShippedIntegrationEventConsumer(
        ILogger<ShipmentShippedIntegrationEventConsumer> logger,
        IPublishEndpoint publishEndpoint)
    {
        _logger = logger;
        _publishEndpoint = publishEndpoint;
    }

    public async Task Consume(
    ConsumeContext<ShipmentShippedIntegrationEvent> context)
    {
        var message = context.Message;
        _logger.LogInformation("Received shipment shipped request for: {Email}", message.TrackingNumber);

        // shipment processing...

        await _publishEndpoint.Publish(new ShipmentShippedIntegrationEvent(
            message.CorrelationId, 
            message.ShipmentId, 
            message.TrackingNumber,
            message.ShippedDate));
    }
}