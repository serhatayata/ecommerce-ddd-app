using Common.Domain.Events.Shippings;
using MassTransit;
using Microsoft.Extensions.Logging;
using MediatR;
using Shipping.Application.Commands.Shipments.Deliver;

namespace Shipping.Application.Consumers;

public class DeliverShipmentRequestEventConsumer : IConsumer<DeliverShipmentRequestEvent>
{
    private readonly ILogger<DeliverShipmentRequestEventConsumer> _logger;
    private readonly IMediator _mediator;

    public DeliverShipmentRequestEventConsumer(
        ILogger<DeliverShipmentRequestEventConsumer> logger,
        IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<DeliverShipmentRequestEvent> context)
    {
        var message = context.Message;
        _logger.LogInformation("Received shipment delivered request for: {TrackingNumber}", 
            message.TrackingNumber);

        try
        {
            _ = await _mediator.Send(new DeliverShipmentCommand(){ ShipmentId = message.ShipmentId });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing shipment delivery for tracking number: {TrackingNumber}", 
                message.TrackingNumber);
        }
    }
}