using AutoMapper;
using PaymentSystem.Application.Models;
using PaymentSystem.Domain.Models;

namespace PaymentSystem.Application.Queries.Payments.Common;

public class PaymentResponse : PaymentModel
{
    public override void Mapping(Profile mapper)
        => mapper
            .CreateMap<Payment, PaymentResponse>()
            .IncludeBase<Payment, PaymentModel>();
}