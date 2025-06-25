using Common.Api.Controllers;
using Common.Application.Models;
using Common.Domain.Events.Shippings;
using Common.Domain.Models.DTOs.Shippings;
using Common.Domain.SharedKernel;
using Common.Domain.ValueObjects;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Shipping.Application.Commands.ShipmentCompanies.Create;
using Shipping.Application.Commands.Shipments.Create;
using Shipping.Application.Commands.Shipments.Deliver;
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
            new ShipmentDto()
            {
                OrderId = command.OrderId,
                ShippingAddress = new AddressDto
                {
                    Street = command.Street,
                    City = command.City,
                    State = command.State,
                    Country = command.Country,
                    ZipCode = command.ZipCode
                },
                TrackingNumber = command.TrackingNumber,
                ShipmentCompanyId = command.ShipmentCompanyId,
                Status = command.Status,
                Items = new List<ShipmentItemDto>()
                {
                    new ShipmentItemDto()
                    {
                        ProductId = random.Next(1, 5),
                        Quantity = random.Next(1, 5)
                    }
                }
            },
            DateTime.Now));

        return Ok();
    }

    [HttpPost]
    [Route("deliver-shipment")]
    public async Task<ActionResult<Result>> DeliverShipment(
    [FromBody] DeliverShipmentCommand command)
        => await Send(command);
    #endregion
}