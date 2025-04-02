using AutoMapper;
using ProductCatalog.Domain.Models.Suppliers;

namespace ProductCatalog.Application.Models.Suppliers;

public class SupplierModel
{
    public string Name { get; set; }
    public string ContactName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Street { get; }
    public string City { get; }
    public string State { get; }
    public string Country { get; }
    public string ZipCode { get; }

    public virtual void Mapping(Profile profile)
    {
        profile.CreateMap<Supplier, SupplierModel>()
            .ForMember(d => d.Street, opt => opt.MapFrom(s => s.Address.Street))
            .ForMember(d => d.City, opt => opt.MapFrom(s => s.Address.City))
            .ForMember(d => d.State, opt => opt.MapFrom(s => s.Address.State))
            .ForMember(d => d.Country, opt => opt.MapFrom(s => s.Address.Country))
            .ForMember(d => d.ZipCode, opt => opt.MapFrom(s => s.Address.ZipCode));
    }
}