using AutoMapper;
using Common.Domain.ValueObjects;
using MediatR;
using Stock.Application.Queries.StockReservations.Common;
using Stock.Domain.Contracts;

namespace Stock.Application.Queries.StockReservations.Details;

public class StockReservationsQuery : IRequest<IEnumerable<StockReservationResponse>>
{
    public int StockId { get; set; }

    public class StockReservationDetailsQueryHandler : IRequestHandler<StockReservationsQuery, IEnumerable<StockReservationResponse>>
    {
        private readonly IStockItemRepository _stockItemRepository;
        private readonly IMapper _mapper;

        public StockReservationDetailsQueryHandler(
            IStockItemRepository stockItemRepository,
            IMapper mapper)
        {
            _stockItemRepository = stockItemRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StockReservationResponse>> Handle(
            StockReservationsQuery request,
            CancellationToken cancellationToken)
        {
            var stockReservation = await _stockItemRepository.GetReservationsByStockItemIdAsync(StockItemId.From(request.StockId), cancellationToken);

            return _mapper.Map<IEnumerable<StockReservationResponse>>(stockReservation);
        }
    }
}