using MediatR;
using Stock.Domain.Contracts;
using Stock.Domain.Models.Stocks;
using ValueObjects = Common.Domain.ValueObjects;

namespace Stock.Application.Queries.StockTransactions.Details;

public class StockTransactionsQuery : IRequest<IEnumerable<StockTransaction>>
{
    public int StockItemId { get; set; }

    public class StockTransactionsQueryHandler : IRequestHandler<StockTransactionsQuery, IEnumerable<StockTransaction>>
    {
        private readonly IStockItemRepository _stockItemRepository;

        public StockTransactionsQueryHandler(IStockItemRepository stockItemRepository)
        {
            _stockItemRepository = stockItemRepository;
        }

        public async Task<IEnumerable<StockTransaction>> Handle(
            StockTransactionsQuery request,
            CancellationToken cancellationToken)
        {
            return await _stockItemRepository.GetTransactionsByStockItemIdAsync(
                ValueObjects.StockItemId.From(request.StockItemId),
                cancellationToken);
        }
    }
}