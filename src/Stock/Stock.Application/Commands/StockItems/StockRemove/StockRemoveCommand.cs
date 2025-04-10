using MediatR;
using Stock.Domain.Contracts;

namespace Stock.Application.Commands.StockItems.StockRemove;

public class StockRemoveCommand : IRequest<StockRemoveResponse>
{
    public int StockItemId { get; set; }
    public int RemovedQuantity { get; set; }

    public class StockRemoveCommandHandler : IRequestHandler<StockRemoveCommand, StockRemoveResponse>
    {
        private readonly IStockItemRepository _stockItemRepository;

        public StockRemoveCommandHandler(
            IStockItemRepository stockItemRepository)
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

            return new StockRemoveResponse
            {
                StockItemId = stockItem.Id,
                Quantity = stockItem.Quantity
            };
            
        }
    }
}