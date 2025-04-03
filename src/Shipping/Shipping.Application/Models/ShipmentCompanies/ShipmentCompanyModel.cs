using AutoMapper;
using Common.Application.Mapping;
using Shipping.Domain.Models.Shipments;

namespace Shipping.Application.Models.ShipmentCompanies;

public class ShipmentCompanyModel : IMapFrom<ShipmentCompany>
{
    public string Name { get; set; }
    public string Code { get; set; }

    public virtual void Mapping(Profile profile)
    {
        profile.CreateMap<ShipmentCompany, ShipmentCompanyModel>();
    }
}