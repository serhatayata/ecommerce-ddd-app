using MediatR;
using Stock.Domain.Contracts;

namespace Stock.Application.Commands.StockReservations.StockReserve;

public class StockReserveCommand : IRequest<StockReserveResponse>
{
    public int StockItemId { get; set; }
    public int ReservedQuantity { get; set; }
    public int OrderId { get; set; }

    public class StockReserveCommandHandler : IRequestHandler<StockReserveCommand, StockReserveResponse>
    {
        private readonly IStockItemRepository _stockItemRepository;

        public StockReserveCommandHandler(IStockItemRepository stockItemRepository)
        {
            _stockItemRepository = stockItemRepository;
        }

        public async Task<StockReserveResponse> Handle(StockReserveCommand request, CancellationToken cancellationToken)
        {
            var stockItem = await _stockItemRepository.GetByIdAsync(request.StockItemId, cancellationToken);

            if (stockItem == null)
                return null;

            stockItem.ReserveStock(request.ReservedQuantity, request.OrderId);
            await _stockItemRepository.SaveAsync(stockItem, cancellationToken);

            return new StockReserveResponse(request.StockItemId, request.ReservedQuantity, request.OrderId);
        }
    }

    
}