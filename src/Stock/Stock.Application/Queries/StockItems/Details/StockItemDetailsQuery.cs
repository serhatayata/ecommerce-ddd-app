using AutoMapper;
using Common.Domain.ValueObjects;
using MediatR;
using Stock.Application.Queries.StockItems.Common;
using Stock.Domain.Contracts;

namespace Stock.Application.Queries.StockItems.Details;

public class StockItemDetailsQuery : IRequest<StockItemResponse>
{
    public int Id { get; set; }

    public class StockItemDetailsQueryHandler : IRequestHandler<StockItemDetailsQuery, StockItemResponse>
    {
        private readonly IStockItemRepository _stockItemRepository;
        private readonly IMapper _mapper;

        public StockItemDetailsQueryHandler(
            IStockItemRepository stockItemRepository,
            IMapper mapper)
        {
            _stockItemRepository = stockItemRepository;
            _mapper = mapper;
        }

        public async Task<StockItemResponse> Handle(
            StockItemDetailsQuery request,
            CancellationToken cancellationToken)
        {
            var stockItem = await _stockItemRepository.GetByIdWithReservationsAsync(StockItemId.From(request.Id), cancellationToken);
            
            return _mapper.Map<StockItemResponse>(stockItem);
        }
    }
}