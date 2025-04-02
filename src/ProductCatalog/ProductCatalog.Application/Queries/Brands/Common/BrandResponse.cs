using AutoMapper;
using ProductCatalog.Application.Models.Brands;
using ProductCatalog.Domain.Models.Brands;

namespace ProductCatalog.Application.Queries.Brands.Common;

public class BrandResponse : BrandModel
{
    public int Id { get; set; }

    public override void Mapping(Profile mapper)
        => mapper
            .CreateMap<Brand, BrandResponse>()
            .IncludeBase<Brand, BrandModel>();    
}