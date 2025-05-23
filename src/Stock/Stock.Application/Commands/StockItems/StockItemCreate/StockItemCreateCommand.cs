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

        public StockItemCreateCommandHandler(
            IStockItemRepository stockItemRepository)
        {
            _stockItemRepository = stockItemRepository;
        }

        public async Task<StockItemCreateResponse> Handle(
            StockItemCreateCommand request, 
            CancellationToken cancellationToken)
        {
            var stockItem = StockItem.Create(
                request.ProductId,
                request.InitialQuantity,
                Location.Create(
                    request.Warehouse,
                    request.Aisle,
                    request.Shelf,
                    request.Bin),
                request.CorrelationId);

            await _stockItemRepository.SaveAsync(stockItem, cancellationToken);

            return new StockItemCreateResponse(stockItem.Id, stockItem.ProductId, stockItem.Quantity);
        }
    }
}