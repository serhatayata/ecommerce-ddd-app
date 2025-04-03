using AutoMapper;
using Shipping.Application.Models.Shipments;
using Shipping.Domain.Models.Shipments;

namespace Shipping.Application.Queries.Shipments.Common;

public class ShipmentResponse : ShipmentModel
{
    public int Id { get; set; }
    
    public override void Mapping(Profile mapper)
        => mapper
            .CreateMap<Shipment, ShipmentResponse>()
            .IncludeBase<Shipment, ShipmentModel>();
}