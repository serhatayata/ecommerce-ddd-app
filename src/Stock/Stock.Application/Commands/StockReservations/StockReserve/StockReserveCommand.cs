using Common.Domain.Models.DTOs.OrderManagements;
using Common.Domain.ValueObjects;
using MassTransit;
using MediatR;
using Stock.Domain.Contracts;
using Stock.Domain.Events;

namespace Stock.Application.Commands.StockReservations.StockReserve;

public class StockReserveCommand : IRequest<StockReserveResponse>, CorrelatedBy<Guid?>
{
    public int OrderId { get; set; }
    public List<OrderItemDto> Items { get; set; }
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

        public async Task<StockReserveResponse> Handle(
        StockReserveCommand request,
        CancellationToken cancellationToken)
        {
            var orderItems = request.Items;

            var reservedItems = await _stockItemRepository.ReserveProductsStocks(
                orderItems.ToDictionary(x => ProductId.From(x.ProductId), x => x.Quantity),
                Common.Domain.ValueObjects.OrderId.From(request.OrderId),
                cancellationToken);

            if (reservedItems.Any())
            {
                await _mediator.Publish(new StockReservedDomainEvent(
                    request.OrderId,
                    DateTime.UtcNow,
                    request.CorrelationId
                ), cancellationToken);

                return new StockReserveResponse(request.OrderId);
            }
            else
            {
                await _mediator.Publish(new StockReserveFailedDomainEvent(
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