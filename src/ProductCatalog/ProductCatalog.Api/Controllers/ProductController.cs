using Common.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Application.Queries.Products.Common;
using ProductCatalog.Application.Queries.Products.Details;

namespace ProductCatalog.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ApiController
{
    [HttpGet]
    [Route(nameof(GetById))]
    public async Task<ActionResult<ProductResponse>> GetById([FromQuery] ProductDetailsQuery query)
        => await Send(query);
}