using MassTransit;
using MediatR;
using Stock.Domain.Contracts;
using Stock.Domain.Events;

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
        private readonly IMediator _mediator;

        public StockReserveCommandHandler(
            IStockItemRepository stockItemRepository,
            IMediator mediator)
        {
            _stockItemRepository = stockItemRepository;
            _mediator = mediator;
        }

        public async Task<StockReserveResponse> Handle(StockReserveCommand request, CancellationToken cancellationToken)
        {
            var stockItem = await _stockItemRepository.GetByIdAsync(request.StockItemId, cancellationToken);

            if (stockItem == null)
                return null;

            stockItem.ReserveStock(
                request.ReservedQuantity, 
                request.OrderId,
                request.CorrelationId);
            var isUpdated = await _stockItemRepository.UpdateAsync(stockItem, cancellationToken) > 0;

            if (!isUpdated)
            {
                await _mediator.Publish(new StockReserveFailedDomainEvent(
                    request.StockItemId,
                    request.OrderId,
                    request.ReservedQuantity,
                    DateTime.UtcNow,
                    "Failed to reserve stock",
                    request.CorrelationId
                ), cancellationToken);

                return null;
            }

            return new StockReserveResponse(request.StockItemId, request.ReservedQuantity, request.OrderId);
        }
    }
}