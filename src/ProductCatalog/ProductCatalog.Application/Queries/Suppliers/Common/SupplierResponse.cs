using AutoMapper;
using ProductCatalog.Application.Models.Suppliers;
using ProductCatalog.Domain.Models.Suppliers;

namespace ProductCatalog.Application.Queries.Suppliers.Common;

public class SupplierResponse : SupplierModel
{
    public int Id { get; set; }

    public override void Mapping(Profile mapper)
        => mapper
            .CreateMap<Supplier, SupplierResponse>()
            .IncludeBase<Supplier, SupplierModel>();    
}