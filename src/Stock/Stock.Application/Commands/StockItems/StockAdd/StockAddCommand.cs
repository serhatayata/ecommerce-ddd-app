using MediatR;
using Stock.Domain.Contracts;
using Stock.Domain.Models.Stocks;

namespace Stock.Application.Commands.StockItems.StockAdd;

public class StockAddCommand : IRequest<StockAddResponse>
{
    public int StockItemId { get; set; }
    public int AddedQuantity { get; set; }

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

            stockItem.AddStock(request.AddedQuantity, "Stock added via command.");
            await _stockItemRepository.SaveAsync(stockItem, cancellationToken);

            return new StockAddResponse
            {
                StockItemId = stockItem.Id,
                Quantity = stockItem.Quantity
            };
        }
    }
}