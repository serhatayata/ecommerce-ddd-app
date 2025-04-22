using Common.Domain.Events.Stocks;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Stock.Application.Commands.StockItems.StockRemove;

namespace Stock.Application.Consumers;

public class StockRemoveRequestEventConsumer : IConsumer<StockRemoveRequestEvent>
{
    private readonly ILogger<StockRemoveRequestEventConsumer> _logger;
    private readonly IMediator _mediator;
    private readonly IPublishEndpoint _publishEndpoint;

    public StockRemoveRequestEventConsumer(
        ILogger<StockRemoveRequestEventConsumer> logger,
        IMediator mediator,
        IPublishEndpoint publishEndpoint)
    {
        _logger = logger;
        _mediator = mediator;
        _publishEndpoint = publishEndpoint;
    }

    public async Task Consume(ConsumeContext<StockRemoveRequestEvent> context)
    {
        var message = context.Message;
        _logger.LogInformation("Received stock remove request for: {StockItemId}", message.StockItemId);

        try
        {
            _ = await _mediator.Send(new StockRemoveCommand()
            {
                StockItemId = message.StockItemId,
                RemovedQuantity = message.RemovedQuantity
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing stock remove for tracking number: {StockItemId}", message.StockItemId);
        }
    }
}