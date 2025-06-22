using Common.Application.Models;
using Common.Domain.SharedKernel;
using MediatR;
using Shipping.Domain.Contracts;
using Shipping.Domain.Models.Shipments;

namespace Shipping.Application.Commands.Shipments.Deliver;

public class DeliverShipmentCommand : IRequest<Result>
{
    public int ShipmentId { get; set; }
    public Guid? CorrelationId { get; set; }


    public class DeliverShipmentCommandHandler : IRequestHandler<DeliverShipmentCommand, Result>
    {
        private readonly IShipmentRepository _shipmentRepository;

        public DeliverShipmentCommandHandler(
        IShipmentRepository shipmentRepository)
        {
            _shipmentRepository = shipmentRepository;
        }

        public async Task<Result> Handle(DeliverShipmentCommand request, CancellationToken cancellationToken)
        {
            var shipment = await _shipmentRepository.GetByIdAsync(request.ShipmentId, cancellationToken);

            if (shipment == null)
                return Result.Failure(null);

            shipment.UpdateStatus(ShipmentStatus.Delivered, request.CorrelationId);

            _ = await _shipmentRepository.SaveAsync(shipment, cancellationToken) > 0;

            return Result.Success;
        }
    }
}