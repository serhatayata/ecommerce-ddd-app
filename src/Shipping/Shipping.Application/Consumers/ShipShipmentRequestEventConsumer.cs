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

    public ShipShipmentRequestEventConsumer(
        ILogger<ShipShipmentRequestEventConsumer> logger,
        IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<ShipShipmentRequestEvent> context)
    {
        var message = context.Message;
        _logger.LogInformation("Received shipment shipped request for: {OrderId}", 
            message.OrderId);

        try
        {
            _ = await _mediator.Send(new ShipShipmentCommand() { ShipmentId = message.OrderId });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing shipment for order ID: {OrderId}", 
                message.OrderId);
        }
    }
}