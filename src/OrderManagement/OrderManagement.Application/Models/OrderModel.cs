using AutoMapper;
using Common.Application.Mapping;
using Common.Domain.ValueObjects;
using OrderManagement.Domain.Models.Orders;

namespace OrderManagement.Application.Models;

public class OrderModel : IMapFrom<Order>
{
    public int UserId { get; set; }
    public DateTime OrderDate { get; set; }
    public int Status { get; set; }

    public HashSet<OrderItemModel> OrderItems { get; set; }

    public virtual void Mapping(Profile profile)
    {
        profile.CreateMap<Order, OrderModel>();
    }
}