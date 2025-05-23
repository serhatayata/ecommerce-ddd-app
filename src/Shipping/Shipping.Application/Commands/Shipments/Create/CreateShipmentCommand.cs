using Common.Domain.ValueObjects;
using MediatR;
using Shipping.Domain.Contracts;
using Shipping.Domain.Models.Shipments;

namespace Shipping.Application.Commands.Shipments.Create;

public class CreateShipmentCommand : IRequest<CreateShipmentResponse>
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

    public class CreateShipmentCommandHandler : IRequestHandler<CreateShipmentCommand, CreateShipmentResponse>
    {
        private readonly IShipmentRepository _shipmentRepository;

        public CreateShipmentCommandHandler(
        IShipmentRepository shipmentRepository)
        {
            _shipmentRepository = shipmentRepository;
        }

        public async Task<CreateShipmentResponse> Handle(CreateShipmentCommand request, CancellationToken cancellationToken)
        {
            var address = new Address(
                request.Street,
                request.City,
                request.State,
                request.Country,
                request.ZipCode);

            var shipment = Shipment.Create(
                Common.Domain.ValueObjects.OrderId.From(request.OrderId),
                address,
                request.TrackingNumber,
                request.ShipmentCompanyId);
                
            var isShipped = request.Status == ShipmentStatus.Shipped;

            if (isShipped)
                shipment.Ship();

            await _shipmentRepository.SaveAsync(shipment, cancellationToken);

            return new CreateShipmentResponse(shipment.Id);
        }
    }
}