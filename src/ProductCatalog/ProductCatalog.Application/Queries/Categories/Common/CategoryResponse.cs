using AutoMapper;
using ProductCatalog.Application.Models.Categories;
using ProductCatalog.Domain.Models.Categories;

namespace ProductCatalog.Application.Queries.Categories.Common;

public class CategoryResponse : CategoryModel
{
    public int Id { get; set; }

    public override void Mapping(Profile mapper)
        => mapper
            .CreateMap<Category, CategoryResponse>()
            .IncludeBase<Category, CategoryModel>();    
}