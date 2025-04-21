using MediatR;
using Stock.Domain.Contracts;
using Common.Domain.Events.Stocks;
using MassTransit;

namespace Stock.Application.Commands.StockItems.StockAdd;

public class StockAddCommand : IRequest<StockAddResponse>, CorrelatedBy<Guid?>
{
    public int StockItemId { get; set; }
    public int AddedQuantity { get; set; }
    public Guid? CorrelationId { get; set; }

    public class StockAddCommandHandler : IRequestHandler<StockAddCommand, StockAddResponse>
    {
        private readonly IStockItemRepository _stockItemRepository;
        private readonly IPublishEndpoint _publishEndpoint;

        public StockAddCommandHandler(
            IStockItemRepository stockItemRepository,
            IPublishEndpoint publishEndpoint)
        {
            _stockItemRepository = stockItemRepository;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<StockAddResponse> Handle(
            StockAddCommand request, 
            CancellationToken cancellationToken)
        {
            var stockItem = await _stockItemRepository.GetByIdAsync(request.StockItemId, cancellationToken);

            if (stockItem == null)
                return null;

            stockItem.AddStock(request.AddedQuantity, "Stock added via command.");
            await _stockItemRepository.UpdateAsync(stockItem, cancellationToken);

            var stockAddedEvent = new StockAddedEvent(
                request.CorrelationId,
                request.StockItemId, 
                request.AddedQuantity,
                DateTime.UtcNow);

            await _publishEndpoint.Publish(stockAddedEvent, cancellationToken);

            return new StockAddResponse
            {
                StockItemId = stockItem.Id,
                Quantity = stockItem.Quantity
            };
        }
    }
}