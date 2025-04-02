using Common.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Application.Commands.Brands.Create;
using ProductCatalog.Application.Commands.Categories.Create;
using ProductCatalog.Application.Commands.Products.Create;
using ProductCatalog.Application.Commands.Suppliers.Create;
using ProductCatalog.Application.Queries.Brands.Common;
using ProductCatalog.Application.Queries.Brands.Details;
using ProductCatalog.Application.Queries.Categories.Common;
using ProductCatalog.Application.Queries.Categories.Details;
using ProductCatalog.Application.Queries.Products.Common;
using ProductCatalog.Application.Queries.Products.Details;
using ProductCatalog.Application.Queries.Suppliers.Common;
using ProductCatalog.Application.Queries.Suppliers.Details;

namespace ProductCatalog.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ApiController
{
    [HttpGet]
    [Route(nameof(GetById))]
    public async Task<ActionResult<ProductResponse>> GetById([FromQuery] ProductDetailsQuery query)
        => await Send(query);

    [HttpPost]
    [Route(nameof(Save))]
    public async Task<ActionResult<CreateProductResponse>> Save([FromBody] CreateProductCommand request)
        => await Send(request);

    #region Brands
    [HttpGet]
    [Route("get-brand-by-id")]
    public async Task<ActionResult<BrandResponse>> GetBrandById([FromQuery] BrandDetailsQuery query)
        => await Send(query);

    [HttpPost]
    [Route("save-brand")]
    public async Task<ActionResult<CreateBrandResponse>> SaveBrand([FromBody] CreateBrandCommand request)
        => await Send(request);
    #endregion

    #region Categories
    [HttpGet]
    [Route("get-category-by-id")]
    public async Task<ActionResult<CategoryResponse>> GetCategoryById([FromQuery] CategoryDetailsQuery query)
        => await Send(query);

    [HttpPost]
    [Route("save-category")]
    public async Task<ActionResult<CreateCategoryResponse>> SaveCategory([FromBody] CreateCategoryCommand request)
        => await Send(request);
    #endregion

    #region Suppliers
    [HttpGet]
    [Route("get-supplier-by-id")]
    public async Task<ActionResult<SupplierResponse>> GetSupplierById([FromQuery] SupplierDetailsQuery query)
        => await Send(query);

    [HttpPost]
    [Route("save-supplier")]
    public async Task<ActionResult<CreateSupplierResponse>> SaveSupplier([FromBody] CreateSupplierCommand request)
        => await Send(request);
    #endregion
}