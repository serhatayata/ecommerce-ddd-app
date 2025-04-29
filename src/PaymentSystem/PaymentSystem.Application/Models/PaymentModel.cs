using AutoMapper;
using Common.Application.Mapping;
using Common.Domain.ValueObjects;
using PaymentSystem.Domain.Models;

namespace PaymentSystem.Application.Models;

public class PaymentModel : IMapFrom<Payment>
{
    public int OrderId { get; set; }
    public decimal Amount { get; set; }
    public PaymentMethod Method { get; set; }
    public PaymentStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }

    public virtual void Mapping(Profile profile)
    {
        profile.CreateMap<Payment, PaymentModel>();
    }
}