using Common.Domain.Events.Stocks;
using MassTransit;
using MediatR;
using Stock.Domain.Contracts;
using Stock.Domain.Models.Stocks;

namespace Stock.Application.Commands.StockItems.StockItemCreate;

public class StockItemCreateCommand : IRequest<StockItemCreateResponse>, CorrelatedBy<Guid?>
{
    public int ProductId { get; set; }
    public int InitialQuantity { get; set; }
    public string Warehouse { get; set; }
    public string Aisle { get; set; }
    public string Shelf { get; set; }
    public string Bin { get; set; }   
    public DateTime CreatedDate { get; set; }
    public Guid? CorrelationId { get; set; }

    public class StockItemCreateCommandHandler : IRequestHandler<StockItemCreateCommand, StockItemCreateResponse>
    {
        private readonly IStockItemRepository _stockItemRepository;
        private readonly IPublishEndpoint _publishEndpoint;

        public StockItemCreateCommandHandler(
            IStockItemRepository stockItemRepository,
            IPublishEndpoint publishEndpoint)
        {
            _stockItemRepository = stockItemRepository;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<StockItemCreateResponse> Handle(
            StockItemCreateCommand request, 
            CancellationToken cancellationToken)
        {
            var stockItem = new StockItem(
                request.ProductId,
                request.InitialQuantity,
                Location.Create(
                    request.Warehouse,
                    request.Aisle,
                    request.Shelf,
                    request.Bin));

            await _stockItemRepository.SaveAsync(stockItem, cancellationToken);

            var stockItemCreatedEvent = new StockItemCreatedEvent(
                request.CorrelationId ?? Guid.NewGuid(),
                stockItem.ProductId,
                stockItem.Quantity,
                stockItem.Location.Warehouse,
                stockItem.Location.Aisle,
                stockItem.Location.Shelf,
                stockItem.Location.Bin,
                request.CreatedDate
            );

            await _publishEndpoint.Publish(stockItemCreatedEvent, cancellationToken);

            return new StockItemCreateResponse(stockItem.Id, stockItem.ProductId, stockItem.Quantity);
        }
    }
}