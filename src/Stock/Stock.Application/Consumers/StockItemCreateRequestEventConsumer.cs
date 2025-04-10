using Common.Domain.Events.Stocks;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Stock.Application.Commands.StockItems.StockItemCreate;

namespace Stock.Application.Consumers;

public class StockItemCreateRequestEventConsumer : IConsumer<StockItemCreateRequestEvent>
{
    private readonly ILogger<StockItemCreateRequestEventConsumer> _logger;
    private readonly IMediator _mediator;
    private readonly IPublishEndpoint _publishEndpoint;

    public StockItemCreateRequestEventConsumer(
        ILogger<StockItemCreateRequestEventConsumer> logger, 
        IMediator mediator, 
        IPublishEndpoint publishEndpoint)
    {
        _logger = logger;
        _mediator = mediator;
        _publishEndpoint = publishEndpoint;
    }

    public async Task Consume(ConsumeContext<StockItemCreateRequestEvent> context)
    {
        var message = context.Message;
        _logger.LogInformation("Received stock item create request for: {StockItemId}", message.StockItemId);

        try
        {
            var result = await _mediator.Send(new StockItemCreateCommand() 
            { 
                ProductId = message.ProductId,
                InitialQuantity = message.Quantity,
                Warehouse = message.Warehouse,
                Aisle = message.Aisle,
                Shelf = message.Shelf,
                Bin = message.Bin,
                CreatedDate = message.CreationDate
            });

            if (result?.Id != null)
                await _publishEndpoint.Publish(new StockItemCreatedEvent(
                    message.CorrelationId,
                    message.ProductId,
                    message.Quantity,
                    message.Warehouse,
                    message.Aisle,
                    message.Shelf,
                    message.Bin,
                    message.CreationDate));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing stock item create request for item ID: {StockItemId}", message.StockItemId);
        }
    }
}