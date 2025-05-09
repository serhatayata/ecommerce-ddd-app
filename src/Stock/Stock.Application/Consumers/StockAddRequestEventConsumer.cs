using Common.Domain.Events.Stocks;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Stock.Application.Commands.StockItems.StockAdd;

namespace Stock.Application.Consumers;

public class StockAddRequestEventConsumer : IConsumer<StockAddRequestEvent>
{
    private readonly ILogger<StockAddRequestEventConsumer> _logger;
    private readonly IMediator _mediator;

    public StockAddRequestEventConsumer(
        ILogger<StockAddRequestEventConsumer> logger, 
        IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }


    public async Task Consume(ConsumeContext<StockAddRequestEvent> context)
    {
        var message = context.Message;
        _logger.LogInformation("Received stock add request for: {StockItemId}", message.StockItemId);

        try
        {
            _ = await _mediator.Send(new StockAddCommand() { StockItemId = message.StockItemId });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing stock add request for item ID: {StockItemId}", message.StockItemId);
        }
    }
}