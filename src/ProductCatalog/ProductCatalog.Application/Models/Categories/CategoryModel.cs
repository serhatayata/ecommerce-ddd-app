using AutoMapper;
using Common.Application.Mapping;
using ProductCatalog.Domain.Models.Categories;

namespace ProductCatalog.Application.Models.Categories;

public class CategoryModel : IMapFrom<Category>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int? ParentId { get; set; }
    
    public virtual void Mapping(Profile profile)
    {
        profile.CreateMap<Category, CategoryModel>();
    }
}