using Common.Application.Models;
using MediatR;
using Shipping.Domain.Contracts;
using Shipping.Domain.Models.Shipments;

namespace Shipping.Application.Commands.Shipments.Ship;

public class ShipShipmentCommand : IRequest<Result>
{
    public Guid OrderId { get; set; }
    public Guid? CorrelationId { get; set; }


    public class ShipShipmentCommandHandler : IRequestHandler<ShipShipmentCommand, Result>
    {
        private readonly IShipmentRepository _shipmentRepository;

        public ShipShipmentCommandHandler(IShipmentRepository shipmentRepository)
        {
            _shipmentRepository = shipmentRepository;
        }

        public async Task<Result> Handle(
        ShipShipmentCommand request,
        CancellationToken cancellationToken)
        {
            

            // shipment.UpdateStatus(ShipmentStatus.Shipped, request.CorrelationId);

            // _ = await _shipmentRepository.SaveAsync(shipment, cancellationToken) > 0;

            return Result.Success;
        }
    }
}
