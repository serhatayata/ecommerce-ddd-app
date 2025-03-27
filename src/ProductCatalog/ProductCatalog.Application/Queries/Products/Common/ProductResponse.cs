using AutoMapper;
using ProductCatalog.Application.Models.Products;
using ProductCatalog.Domain.Models.Products;

namespace ProductCatalog.Application.Queries.Products.Common;

public class ProductResponse : ProductModel
{
    public int Id { get; set; }
    
    public override void Mapping(Profile mapper)
        => mapper
            .CreateMap<Product, ProductResponse>()
            .IncludeBase<Product, ProductModel>();
}