using Common.Application.Models;
using Common.Domain.Models.DTOs.Shippings;
using Common.Domain.SharedKernel;
using MediatR;
using Shipping.Domain.Contracts;
using Shipping.Domain.Models.Shipments;

namespace Shipping.Application.Commands.Shipments.Ship;

public class ShipShipmentCommand : IRequest<Result>
{
    public Guid OrderId { get; set; }
    public Guid? CorrelationId { get; set; }
    public ShipmentDto ShipmentDetail { get; set; }


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
            // Shipment ship processing for 3th party B2B

            var trackingNumber = GenerateTrackingNumber();

            var shipmentDetail = request.ShipmentDetail;

            var shipment = Shipment.Create(
                Common.Domain.ValueObjects.OrderId.From(request.OrderId),
                shipmentDetail.ShippingAddress,
                trackingNumber,
                shipmentDetail.ShipmentCompanyId
                );

            shipment.UpdateStatus(ShipmentStatus.Shipped, request.CorrelationId);

            _ = await _shipmentRepository.SaveAsync(shipment, cancellationToken) > 0;

            return Result.Success;
        }

        private static string GenerateTrackingNumber()
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 12)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
