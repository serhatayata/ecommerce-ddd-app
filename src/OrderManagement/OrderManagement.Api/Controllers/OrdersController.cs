using Common.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Application.Commands;
using OrderManagement.Application.Queries.Common;
using OrderManagement.Application.Queries.Details;

namespace OrderManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : BaseApiController
{
    [HttpGet]
    [Route("detail")]
    public async Task<ActionResult<OrderResponse>> GetOrderDetails(
    [FromQuery] OrderDetailsQuery query)
        => await Send(query);

    [HttpPost]
    [Route(nameof(AddOrder))]
    public async Task<ActionResult<OrderAddResponse>> AddOrder(
    [FromBody] OrderAddCommand request)
        => await Send(request);
}