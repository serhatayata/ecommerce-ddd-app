using AutoMapper;
using OrderManagement.Application.Models;
using OrderManagement.Domain.Models.Orders;

namespace OrderManagement.Application.Queries.Common;

public class OrderResponse : OrderModel
{
    public int Id { get; set; }
    
    public override void Mapping(Profile mapper)
        => mapper
            .CreateMap<Order, OrderResponse>()
            .IncludeBase<Order, OrderModel>();
}