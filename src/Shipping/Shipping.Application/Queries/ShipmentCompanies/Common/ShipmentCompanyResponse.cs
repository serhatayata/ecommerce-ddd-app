using AutoMapper;
using Shipping.Application.Models.ShipmentCompanies;
using Shipping.Domain.Models.Shipments;

namespace Shipping.Application.Queries.ShipmentCompanies.Common;

public class ShipmentCompanyResponse : ShipmentCompanyModel
{
    public int Id { get; set; }
    
    public override void Mapping(Profile mapper)
        => mapper
            .CreateMap<ShipmentCompany, ShipmentCompanyResponse>()
            .IncludeBase<ShipmentCompany, ShipmentCompanyModel>();
}