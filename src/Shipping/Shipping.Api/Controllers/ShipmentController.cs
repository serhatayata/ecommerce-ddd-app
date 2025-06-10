using Common.Api.Controllers;
using Common.Application.Models;
using Common.Domain.Events.Shippings;
using Common.Domain.ValueObjects;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Shipping.Application.Commands.ShipmentCompanies.Create;
using Shipping.Application.Commands.Shipments.Create;
using Shipping.Application.Queries.ShipmentCompanies.Common;
using Shipping.Application.Queries.ShipmentCompanies.Details;
using Shipping.Domain.Models.Shipments;

namespace Shipping.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShipmentController : BaseApiController
{
    private readonly IPublishEndpoint _publishEndpoint;

    public ShipmentController(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    #region Shipment Company
    [HttpGet]
    [Route("shipment-company")]
    public async Task<ActionResult<ShipmentCompanyResponse>> GetShipmentCompanyDetails(
    [FromQuery] ShipmentCompanyDetailsQuery query)
        => await Send(query);

    [HttpPost]
    [Route("create-shipment-company")]
    public async Task<ActionResult<CreateShipmentCompanyResponse>> CreateShipmentCompany(
    [FromBody] CreateShipmentCompanyCommand command)
        => await Send(command);
    #endregion

    #region Shipment Test
    [HttpPost]
    [Route("ship-event-test")]
    public async Task<ActionResult<Result>> CreateShipEvent()
    {
        var random = new Random();
        var command = new CreateShipmentCommand()
        {
            OrderId = Guid.NewGuid(),
            Street = $"Street {random.Next(1, 100)}",
            City = "Random City",
            State = "Random State",
            Country = "Random Country",
            ZipCode = random.Next(10000, 99999).ToString(),
            TrackingNumber = $"TRK{random.Next(100000, 999999)}",
            ShipmentCompanyId = random.Next(1, 5),
            Status = (ShipmentStatus)random.Next(0, 2)
        };

        await Send(command);

        await _publishEndpoint.Publish(new ShipShipmentRequestEvent(
            Guid.NewGuid(),
            command.OrderId,
            DateTime.Now));

        return Ok();
    }
    #endregion
}