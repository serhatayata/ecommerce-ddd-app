using AutoMapper;
using Common.Application.Mapping;
using ProductCatalog.Domain.Models.Products;

namespace ProductCatalog.Application.Models.Products;

public class ProductModel : IMapFrom<Product>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int BrandId { get; set; }
    public int CategoryId { get; set; }
    public int SupplierId { get; set; }
    public ProductStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public virtual void Mapping(Profile profile)
    {
        profile.CreateMap<Product, ProductModel>()
            .ForMember(d => d.Price, opt => opt.MapFrom(s => s.Price.Amount));
    }
}