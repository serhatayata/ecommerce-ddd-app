using AutoMapper;
using Common.Application.Mapping;
using Stock.Domain.Models.Stocks;

namespace Stock.Application.Models.StockItems;

public class StockItemModel : IMapFrom<StockItem>
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public string Warehouse { get; set; }
    public string Aisle { get; set; }
    public string Shelf { get; set; }
    public string Bin { get; }    
    public StockStatus Status { get; set; }
    public DateTime LastUpdated { get; set; }

    public virtual void Mapping(Profile profile)
    {
        profile.CreateMap<StockItem, StockItemModel>()
            .ForMember(d => d.Warehouse, opt => opt.MapFrom(s => s.Location.Warehouse))
            .ForMember(d => d.Aisle, opt => opt.MapFrom(s => s.Location.Aisle))
            .ForMember(d => d.Shelf, opt => opt.MapFrom(s => s.Location.Shelf))
            .ForMember(d => d.Bin, opt => opt.MapFrom(s => s.Location.Bin));
    }
}