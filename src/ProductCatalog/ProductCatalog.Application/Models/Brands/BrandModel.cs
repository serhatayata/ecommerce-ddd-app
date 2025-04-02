using AutoMapper;
using Common.Application.Mapping;
using ProductCatalog.Domain.Models.Brands;

namespace ProductCatalog.Application.Models.Brands;

public class BrandModel  : IMapFrom<Brand>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Website { get; set; }

    public virtual void Mapping(Profile profile)
    {
        profile.CreateMap<Brand, BrandModel>();
    }
}