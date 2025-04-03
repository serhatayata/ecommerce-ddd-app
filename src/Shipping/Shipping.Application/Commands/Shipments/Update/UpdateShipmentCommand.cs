using AutoMapper;
using MediatR;
using Shipping.Domain.Contracts;
using Shipping.Domain.Models.Shipments;

namespace Shipping.Application.Commands.Shipments.Update;

public class UpdateShipmentCommand : IRequest<UpdateShipmentResponse>
{
    public int ShipmentCompanyId { get; set; }
    public ShipmentStatus Status { get; set; }

    public class UpdateShipmentCommandHandler : IRequestHandler<UpdateShipmentCommand, UpdateShipmentResponse>
    {
        private readonly IShipmentRepository _shipmentRepository;
        private readonly IMapper _mapper;

        public UpdateShipmentCommandHandler(
        IShipmentRepository shipmentRepository,
        IMapper mapper)
        {
            _mapper = mapper;
            _shipmentRepository = shipmentRepository;
        }

        public async Task<UpdateShipmentResponse> Handle(
        UpdateShipmentCommand request, 
        CancellationToken cancellationToken)
        {
            var shipment = await _shipmentRepository.GetByIdAsync(request.ShipmentCompanyId, cancellationToken);

            if (shipment == null)
                return null;

            if (request.Status != ShipmentStatus.None)
                shipment.UpdateStatus(request.Status);

            if (request.ShipmentCompanyId > 0)
                shipment.UpdateShipmentCompany(request.ShipmentCompanyId);
                
            await _shipmentRepository.UpdateAsync(shipment, cancellationToken);

            var result = _mapper.Map<UpdateShipmentResponse>(shipment);
            return result;
        }
    }
}