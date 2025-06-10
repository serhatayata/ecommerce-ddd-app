using AutoMapper;
using Common.Application.Mapping;
using Stock.Domain.Models.Stocks;

namespace Stock.Application.Models.StockReservations;

public class StockReservationModel : IMapFrom<StockReservation>
{
    public int StockItemId { get; set; }
    public Guid OrderId { get; set; }
    public int Quantity { get; set; }
    public DateTime ReservationDate { get; set; }
    public ReservationStatus Status { get; set; }

    public virtual void Mapping(Profile profile)
    {
        profile.CreateMap<StockReservation, StockReservationModel>();
    }
}