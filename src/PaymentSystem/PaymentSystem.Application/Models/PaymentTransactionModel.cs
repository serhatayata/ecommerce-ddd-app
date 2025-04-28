using AutoMapper;
using Common.Application.Mapping;
using PaymentSystem.Domain.Models;

namespace PaymentSystem.Application.Models;

public class PaymentTransactionModel : IMapFrom<PaymentTransaction>
{
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string TransactionId { get; set; }
    public int PaymentId { get; set; }

    public virtual void Mapping(Profile profile)
    {
        profile.CreateMap<PaymentTransaction, PaymentTransactionModel>();
    }    
}