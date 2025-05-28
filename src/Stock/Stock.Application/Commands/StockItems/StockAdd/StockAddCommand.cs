using MediatR;
using Stock.Domain.Contracts;
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

        public StockAddCommandHandler(
            IStockItemRepository stockItemRepository)
        {
            _stockItemRepository = stockItemRepository;
        }

        public async Task<StockAddResponse> Handle(
            StockAddCommand request, 
            CancellationToken cancellationToken)
        {
            var stockItem = await _stockItemRepository.GetByIdAsync(request.StockItemId, cancellationToken);

            if (stockItem == null)
                return null;

            stockItem.AddStock(
                request.AddedQuantity, 
                "Stock added via command.");

            stockItem.RaiseStockAddedDomainEvent(request.CorrelationId);

            await _stockItemRepository.UpdateAsync(stockItem, cancellationToken);

            return new StockAddResponse
            {
                StockItemId = stockItem.Id,
                Quantity = stockItem.Quantity
            };
        }
    }
}