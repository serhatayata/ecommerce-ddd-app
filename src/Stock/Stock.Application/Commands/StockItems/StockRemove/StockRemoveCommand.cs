using Common.Domain.Events.Stocks;
using MassTransit;
using MediatR;
using Stock.Domain.Contracts;

namespace Stock.Application.Commands.StockItems.StockRemove;

public class StockRemoveCommand : IRequest<StockRemoveResponse>, CorrelatedBy<Guid?>
{
    public int StockItemId { get; set; }
    public int RemovedQuantity { get; set; }
    public Guid? CorrelationId { get; set; }

    public class StockRemoveCommandHandler : IRequestHandler<StockRemoveCommand, StockRemoveResponse>
    {
        private readonly IStockItemRepository _stockItemRepository;
        private readonly IPublishEndpoint _publishEndpoint;

        public StockRemoveCommandHandler(
            IStockItemRepository stockItemRepository,
            IPublishEndpoint publishEndpoint)
        {
            _stockItemRepository = stockItemRepository;
        }

        public async Task<StockRemoveResponse> Handle(
            StockRemoveCommand request, 
            CancellationToken cancellationToken)
        {
            var stockItem = await _stockItemRepository.GetByIdAsync(request.StockItemId, cancellationToken);

            if (stockItem == null)
                return null;

            stockItem.RemoveStock(request.RemovedQuantity, "Stock removed via command.");
            await _stockItemRepository.SaveAsync(stockItem, cancellationToken);

            var stockRemovedEvent = new StockRemovedEvent(
                request.CorrelationId ?? Guid.NewGuid(),
                request.StockItemId,
                stockItem.Quantity,
                DateTime.UtcNow);
                
            await _publishEndpoint.Publish(stockRemovedEvent, cancellationToken);

            return new StockRemoveResponse
            {
                StockItemId = stockItem.Id,
                Quantity = stockItem.Quantity
            };
            
        }
    }
}