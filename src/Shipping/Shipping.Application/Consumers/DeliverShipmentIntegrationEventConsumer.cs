using Common.Domain.Events.Shippings;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Shipping.Application.Consumers;

public class DeliverShipmentIntegrationEventConsumer : IConsumer<DeliverShipmentIntegrationEvent>
{
    private readonly ILogger<DeliverShipmentIntegrationEventConsumer> _logger;
    private readonly IPublishEndpoint _publishEndpoint;

    public DeliverShipmentIntegrationEventConsumer(
    ILogger<DeliverShipmentIntegrationEventConsumer> logger,
    IPublishEndpoint publishEndpoint)
    {
        _logger = logger;
        _publishEndpoint = publishEndpoint;
    }

    public async Task Consume(
    ConsumeContext<DeliverShipmentIntegrationEvent> context)
    {
        var message = context.Message;
        _logger.LogInformation("Received shipment delivered request for: {Email}", message.TrackingNumber);

        // shipment processing...

        BURADA delivered yapalım db de

        await _publishEndpoint.Publish(new ShipmentDeliveredIntegrationEvent(
            message.CorrelationId, 
            message.ShipmentId, 
            message.TrackingNumber,
            message.DeliveredDate));
    }
}