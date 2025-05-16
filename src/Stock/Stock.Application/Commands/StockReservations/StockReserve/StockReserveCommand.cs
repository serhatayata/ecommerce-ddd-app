using Common.Domain.Models.DTOs.OrderManagements;
using MassTransit;
using MediatR;
using Stock.Domain.Contracts;
using Stock.Domain.Events;

namespace Stock.Application.Commands.StockReservations.StockReserve;

public class StockReserveCommand : IRequest<StocksReserveResponse>, CorrelatedBy<Guid?>
{
    public int OrderId { get; set; }
    public List<OrderItemDto> Items { get; set; }
    public Guid? CorrelationId { get; set; }

    public class StockReserveCommandHandler : IRequestHandler<StockReserveCommand, StocksReserveResponse>
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

        public async Task<StocksReserveResponse> Handle(StockReserveCommand request, CancellationToken cancellationToken)
        {
            var orderItems = request.Items;
            var productIds = orderItems.Select(x => x.ProductId).ToList();

            var stockItems = await _stockItemRepository.GetByProductIdsAsync(
                productIds, 
                cancellationToken);

            var reservedItems = new List<(int StockItemId, int Quantity)>();

            foreach (var orderItem in orderItems)
            {
                var stockItem = stockItems.FirstOrDefault(x => x.ProductId == orderItem.ProductId);
                if (stockItem != null)
                {
                    stockItem.ReserveStock(orderItem.Quantity, request.OrderId);

                    var isUpdated = await _stockItemRepository.UpdateAsync(stockItem, cancellationToken) > 0;

                    if (isUpdated)
                        reservedItems.Add((stockItem.Id, orderItem.Quantity));
                }
            }

            if (reservedItems.Any())
            {
                await _mediator.Publish(new StocksReservedDomainEvent(
                    request.OrderId,
                    DateTime.UtcNow,
                    request.CorrelationId
                ), cancellationToken);

                return new StocksReserveResponse(request.OrderId);
            }
            else
            {
                await _mediator.Publish(new StocksReserveFailedDomainEvent(
                    request.OrderId,
                    DateTime.UtcNow,
                    "Failed to reserve stock for all items",
                    request.CorrelationId
                ), cancellationToken);

                return null;
            }
        }
    }
}