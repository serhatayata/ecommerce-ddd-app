using Common.Application.Models;
using MediatR;
using Shipping.Domain.Contracts;
using Shipping.Domain.Models.Shipments;

namespace Shipping.Application.Commands.Shipments.Ship;

public class ShipShipmentCommand : IRequest<Result>
{
    public int ShipmentId { get; set; }

    public class ShipShipmentCommandHandler : IRequestHandler<ShipShipmentCommand, Result>
    {
        private readonly IShipmentRepository _shipmentRepository;

        public ShipShipmentCommandHandler(IShipmentRepository shipmentRepository)
        {
            _shipmentRepository = shipmentRepository;
        }

        public async Task<Result> Handle(ShipShipmentCommand request, CancellationToken cancellationToken)
        {
            var shipment = await _shipmentRepository.GetByIdAsync(request.ShipmentId, cancellationToken);

            if (shipment == null)
                return Result.Failure(null);

            shipment.UpdateStatus(ShipmentStatus.Shipped);

            await _shipmentRepository.SaveAsync(shipment, cancellationToken);

            return Result.Success;
        }
    }
}
