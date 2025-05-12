using Common.Application.Models;
using MediatR;
using Shipping.Domain.Contracts;
using Shipping.Domain.Events;
using Shipping.Domain.Models.Shipments;

namespace Shipping.Application.Commands.Shipments.Deliver;

public class DeliverShipmentCommand : IRequest<Result>
{
    public int ShipmentId { get; set; }
    public Guid? CorrelationId { get; set; }


    public class DeliverShipmentCommandHandler : IRequestHandler<DeliverShipmentCommand, Result>
    {
        private readonly IShipmentRepository _shipmentRepository;
        private readonly IMediator _mediator;

        public DeliverShipmentCommandHandler(
        IShipmentRepository shipmentRepository,
        IMediator mediator)
        {
            _shipmentRepository = shipmentRepository;
            _mediator = mediator;
        }

        public async Task<Result> Handle(DeliverShipmentCommand request, CancellationToken cancellationToken)
        {
            var shipment = await _shipmentRepository.GetByIdAsync(request.ShipmentId, cancellationToken);

            if (shipment == null)
                return Result.Failure(null);

            shipment.UpdateStatus(ShipmentStatus.Delivered, request.CorrelationId);

            var isDelivered = await _shipmentRepository.SaveAsync(shipment, cancellationToken) > 0;

            if (!isDelivered)
                await _mediator.Publish(new ShipmentDeliverFailedDomainEvent(
                    request.ShipmentId,
                    DateTime.UtcNow,
                    "Failed to deliver shipment", 
                    request.CorrelationId), 
                    cancellationToken);

            return Result.Success;
        }
    }
}