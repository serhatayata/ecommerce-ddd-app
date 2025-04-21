using MassTransit;
using MediatR;
using Stock.Domain.Contracts;

namespace Stock.Application.Commands.StockReservations.StockReserve;

public class StockReserveCommand : IRequest<StockReserveResponse>, CorrelatedBy<Guid?>
{
    public int StockItemId { get; set; }
    public int ReservedQuantity { get; set; }
    public int OrderId { get; set; }
    public Guid? CorrelationId { get; set; }

    public class StockReserveCommandHandler : IRequestHandler<StockReserveCommand, StockReserveResponse>
    {
        private readonly IStockItemRepository _stockItemRepository;

        public StockReserveCommandHandler(IStockItemRepository stockItemRepository)
        {
            _stockItemRepository = stockItemRepository;
        }

        public async Task<StockReserveResponse> Handle(StockReserveCommand request, CancellationToken cancellationToken)
        {
            BURADA Integration event atıcaz

            var stockItem = await _stockItemRepository.GetByIdAsync(request.StockItemId, cancellationToken);

            if (stockItem == null)
                return null;

            stockItem.ReserveStock(request.ReservedQuantity, request.OrderId);
            await _stockItemRepository.UpdateAsync(stockItem, cancellationToken);

            return new StockReserveResponse(request.StockItemId, request.ReservedQuantity, request.OrderId);
        }
    }

    
}