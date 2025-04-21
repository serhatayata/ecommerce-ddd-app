using Common.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Stock.Application.Commands.StockItems.StockAdd;
using Stock.Application.Commands.StockItems.StockItemCreate;
using Stock.Application.Commands.StockReservations.StockReserve;
using Stock.Application.Queries.StockItems.Common;
using Stock.Application.Queries.StockItems.Details;
using Stock.Application.Queries.StockReservations.Common;
using Stock.Application.Queries.StockReservations.Details;
using Stock.Application.Queries.StockTransactions.Details;
using Stock.Domain.Models.Stocks;

namespace Stock.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StocksController : ApiController
{
    [HttpPost]
    [Route("add")]
    public async Task<ActionResult<StockAddResponse>> AddStockItem(
    [FromBody] StockAddCommand command)
        => await Send(command);

    [HttpPost]
    [Route("reserve")]
    public async Task<ActionResult<StockReserveResponse>> ReserveStock(
        [FromBody] StockReserveCommand command)
        => await Send(command);

    [HttpPost]
    [Route("create")]
    public async Task<ActionResult<StockItemCreateResponse>> CreateStockItem(
        [FromBody] StockItemCreateCommand command)
        => await Send(command);

    [HttpGet("{id}")]
    public async Task<ActionResult<StockItemResponse>> GetStockItem(int id)
        => await Send(new StockItemDetailsQuery { Id = id });

    [HttpGet("{id}/transactions")]
    public async Task<ActionResult<IEnumerable<StockTransaction>>> GetTransactions(int id)
        => await Send(new StockTransactionsQuery { StockItemId = id });

    [HttpGet("{id}/reservations")]
    public async Task<ActionResult<IEnumerable<StockReservationResponse>>> GetReservations(int id)
        => await Send(new StockReservationsQuery { StockId = id });
}