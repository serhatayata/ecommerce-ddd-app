using AutoMapper;
using Stock.Application.Models.StockItems;
using Stock.Domain.Models.Stocks;

namespace Stock.Application.Queries.StockItems.Common;

public class StockItemResponse : StockItemModel
{
    public int Id { get; set; }
    public int AvailableQuantity { get; set; }
    
    public override void Mapping(Profile mapper)
        => mapper
            .CreateMap<StockItem, StockItemResponse>()
            .ForMember(s => s.AvailableQuantity, opt => opt.MapFrom(src => src.GetAvailableQuantity()))
            .IncludeBase<StockItem, StockItemModel>();
}