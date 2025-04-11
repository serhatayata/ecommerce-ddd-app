using AutoMapper;
using Common.Application.Mapping;
using Stock.Domain.Models.Stocks;

namespace Stock.Application.Models.StockTransactions;

public class StockTransactionModel : IMapFrom<StockTransaction>
{
    public int StockItemId { get; set; }
    public int Quantity { get; set; }
    public StockTransactionType Type { get; set; }
    public string Reason { get; set; }
    public DateTime TransactionDate { get; set; }

    public virtual void Mapping(Profile profile)
    {
        profile.CreateMap<StockTransaction, StockTransactionModel>();
    }
}