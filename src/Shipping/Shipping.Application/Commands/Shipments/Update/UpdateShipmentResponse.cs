using AutoMapper;
using Shipping.Application.Models.Shipments;
using Shipping.Domain.Models.Shipments;

namespace Shipping.Application.Commands.Shipments.Update;

public class UpdateShipmentResponse : ShipmentModel
{
    public override void Mapping(Profile mapper)
        => mapper
            .CreateMap<Shipment, UpdateShipmentResponse>()
            .IncludeBase<Shipment, ShipmentModel>();
}