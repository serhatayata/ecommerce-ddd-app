using AutoMapper;
using PaymentSystem.Application.Models;
using PaymentSystem.Domain.Models;

namespace PaymentSystem.Application.Queries.PaymentTransactions.Common;

public class PaymentTransactionResponse : PaymentTransactionModel
{
    public override void Mapping(Profile mapper)
        => mapper
            .CreateMap<PaymentTransaction, PaymentTransactionResponse>()
            .IncludeBase<PaymentTransaction, PaymentTransactionModel>();
}