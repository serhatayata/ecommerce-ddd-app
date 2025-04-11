using AutoMapper;
using Stock.Application.Models.StockReservations;
using Stock.Domain.Models.Stocks;

namespace Stock.Application.Queries.StockReservations.Common;

public class StockReservationResponse : StockReservationModel
{
    public int Id { get; set; }

    public override void Mapping(Profile mapper)
        => mapper
            .CreateMap<StockReservation, StockReservationResponse>()
            .IncludeBase<StockReservation, StockReservationModel>();
}