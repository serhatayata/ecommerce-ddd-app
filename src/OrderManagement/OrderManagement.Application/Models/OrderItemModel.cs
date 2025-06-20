using AutoMapper;
using Common.Application.Mapping;
using Common.Domain.ValueObjects;
using OrderManagement.Domain.Models.Orders;

namespace OrderManagement.Application.Models;

public class OrderItemModel : IMapFrom<OrderItem>
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }

    public virtual void Mapping(Profile profile)
    {
        profile.CreateMap<OrderItem, OrderItemModel>()
            .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice.Amount));
    }
}
