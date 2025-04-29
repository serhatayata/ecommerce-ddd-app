using Common.Domain.Events.Stocks;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Stock.Application.Commands.StockReservations.StockReserve;

namespace Stock.Application.Consumers;

public class StockReserveRequestEventConsumer : IConsumer<StockReserveRequestEvent>
{
    private readonly ILogger<StockReserveRequestEventConsumer> _logger;
    private readonly IMediator _mediator;

    public StockReserveRequestEventConsumer(
        ILogger<StockReserveRequestEventConsumer> logger, 
        IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<StockReserveRequestEvent> context)
    {
        var message = context.Message;
        _logger.LogInformation("Received stock reserve request for: Order : {OrderId} - {StockItemId}", 
        message.StockItemId, message.OrderId);   

        try
        {
            _ = await _mediator.Send(new StockReserveCommand() 
            {  
                StockItemId = message.StockItemId, 
                ReservedQuantity = message.ReservedQuantity, 
                OrderId = message.OrderId
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing stock reserve request for item ID: {StockItemId}", message.StockItemId);
        }
    }
}