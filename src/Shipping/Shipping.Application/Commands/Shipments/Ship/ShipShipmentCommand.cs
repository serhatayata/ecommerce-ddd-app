using Common.Application.Models;
using MediatR;
using Shipping.Domain.Contracts;
using Shipping.Domain.Events;
using Shipping.Domain.Models.Shipments;

namespace Shipping.Application.Commands.Shipments.Ship;

public class ShipShipmentCommand : IRequest<Result>
{
    public int ShipmentId { get; set; }
    public Guid? CorrelationId { get; set; }


    public class ShipShipmentCommandHandler : IRequestHandler<ShipShipmentCommand, Result>
    {
        private readonly IShipmentRepository _shipmentRepository;
        private readonly IMediator _mediator;

        public ShipShipmentCommandHandler(IShipmentRepository shipmentRepository, IMediator mediator)
        {
            _shipmentRepository = shipmentRepository;
            _mediator = mediator;
        }

        public async Task<Result> Handle(ShipShipmentCommand request, CancellationToken cancellationToken)
        {
            var shipment = await _shipmentRepository.GetByIdAsync(request.ShipmentId, cancellationToken);

            if (shipment == null)
                return Result.Failure(null);

            shipment.UpdateStatus(ShipmentStatus.Shipped, request.CorrelationId);

            var isReserved = await _shipmentRepository.SaveAsync(shipment, cancellationToken) > 0;

            if (!isReserved)
                await _mediator.Publish(new ShipmentShipFailedDomainEvent(
                    request.ShipmentId,
                    DateTime.UtcNow,
                    "Failed to ship shipment", 
                    request.CorrelationId), 
                    cancellationToken);

            return Result.Success;
        }
    }
}
