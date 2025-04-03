using Common.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Shipping.Application.Commands.ShipmentCompanies.Create;
using Shipping.Application.Queries.ShipmentCompanies.Common;
using Shipping.Application.Queries.ShipmentCompanies.Details;

namespace Shipping.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShipmentController : ApiController
{
    #region Shipment Company
    [HttpGet]
    [Route("shipment-company")]
    public async Task<ActionResult<ShipmentCompanyResponse>> GetShipmentCompanyDetails(
    ShipmentCompanyDetailsQuery query)
        => await Send(query);

    [HttpPost]
    [Route("create-shipment-company")]
    public async Task<ActionResult<CreateShipmentCompanyResponse>> CreateShipmentCompany(
    CreateShipmentCompanyCommand command)
        => await Send(command);
    #endregion
}