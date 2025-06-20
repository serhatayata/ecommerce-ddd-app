using AutoMapper;
using OrderManagement.Application.Models;
using OrderManagement.Domain.Models.Orders;

namespace OrderManagement.Application.Queries.Common;

public class OrderResponse : OrderModel
{
    public Guid Id { get; set; }
    
    public override void Mapping(Profile mapper)
        => mapper
            .CreateMap<Order, OrderResponse>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId.Value))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.Value))
            .IncludeBase<Order, OrderModel>();
}