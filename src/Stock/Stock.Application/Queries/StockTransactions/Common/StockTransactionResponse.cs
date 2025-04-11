using AutoMapper;
using Stock.Application.Models.StockTransactions;
using Stock.Domain.Models.Stocks;

namespace Stock.Application.Queries.StockTransactions.Common;

public class StockTransactionResponse : StockTransactionModel
{
    public int Id { get; set; }

    public override void Mapping(Profile mapper)
        => mapper
            .CreateMap<StockTransaction, StockTransactionResponse>()
            .IncludeBase<StockTransaction, StockTransactionModel>();    
}