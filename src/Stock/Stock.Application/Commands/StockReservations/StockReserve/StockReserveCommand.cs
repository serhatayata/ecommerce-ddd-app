using MassTransit;
using MediatR;
using Stock.Domain.Contracts;
using Common.Domain.Events.Stocks;

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
        private readonly IPublishEndpoint _publishEndpoint;

        public StockReserveCommandHandler(
            IStockItemRepository stockItemRepository,
            IPublishEndpoint publishEndpoint)
        {
            _stockItemRepository = stockItemRepository;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<StockReserveResponse> Handle(StockReserveCommand request, CancellationToken cancellationToken)
        {
            var stockItem = await _stockItemRepository.GetByIdAsync(request.StockItemId, cancellationToken);

            if (stockItem == null)
                return null;

            stockItem.ReserveStock(request.ReservedQuantity, request.OrderId);
            await _stockItemRepository.UpdateAsync(stockItem, cancellationToken);

            var stockReservedEvent = new StockReservedEvent(
                request.CorrelationId ?? Guid.NewGuid(),
                request.StockItemId,
                request.OrderId,
                request.ReservedQuantity,
                DateTime.UtcNow
            );

            await _publishEndpoint.Publish(stockReservedEvent, cancellationToken);

            return new StockReserveResponse(request.StockItemId, request.ReservedQuantity, request.OrderId);
        }
    }
}