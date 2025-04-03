using AutoMapper;
using Common.Application.Mapping;
using Shipping.Domain.Models.Shipments;

namespace Shipping.Application.Models.Shipments;

public class ShipmentModel : IMapFrom<Shipment>
{
    public int OrderId { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
    public string ZipCode { get; set; }
    public string TrackingNumber { get; set; }
    public int ShipmentCompanyId { get; set; }
    public ShipmentStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ShippedAt { get; set; }
    public DateTime? DeliveredAt { get; set; }

    public virtual void Mapping(Profile profile)
    {
        profile.CreateMap<Shipment, ShipmentModel>()
            .ForMember(d => d.Street, opt => opt.MapFrom(s => s.ShippingAddress.Street))
            .ForMember(d => d.City, opt => opt.MapFrom(s => s.ShippingAddress.City))
            .ForMember(d => d.State, opt => opt.MapFrom(s => s.ShippingAddress.State))
            .ForMember(d => d.Country, opt => opt.MapFrom(s => s.ShippingAddress.Country))
            .ForMember(d => d.ZipCode, opt => opt.MapFrom(s => s.ShippingAddress.ZipCode));
    }
}