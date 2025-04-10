using Common.Domain.Events.Stocks;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Stock.Application.Commands.StockItems.StockReserve;

namespace Stock.Application.Consumers;

public class StockReserveRequestEventConsumer : IConsumer<StockReserveRequestEvent>
{
    private readonly ILogger<StockReserveRequestEventConsumer> _logger;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IMediator _mediator;

    public StockReserveRequestEventConsumer(
        ILogger<StockReserveRequestEventConsumer> logger, 
        IMediator mediator, 
        IPublishEndpoint publishEndpoint)
    {
        _logger = logger;
        _mediator = mediator;
        _publishEndpoint = publishEndpoint;
    }

    public async Task Consume(ConsumeContext<StockReserveRequestEvent> context)
    {
        var message = context.Message;
        _logger.LogInformation("Received stock reserve request for: Order : {OrderId} - {StockItemId}", 
        message.StockItemId, message.OrderId);   

        try
        {
            var result = await _mediator.Send(new StockReserveCommand() 
            {  
                StockItemId = message.StockItemId, 
                ReservedQuantity = message.ReservedQuantity, 
                OrderId = message.OrderId
            });

            if (result != null)
                await _publishEndpoint.Publish(new StockReservedEvent(
                    message.CorrelationId,
                    message.StockItemId,
                    message.OrderId,
                    message.ReservedQuantity,
                    message.CreationDate));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing stock reserve request for item ID: {StockItemId}", message.StockItemId);
        }
    }
}