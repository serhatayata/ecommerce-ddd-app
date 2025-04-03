using AutoMapper;
using MediatR;
using Shipping.Application.Queries.Shipments.Common;
using Shipping.Domain.Contracts;

namespace Shipping.Application.Queries.Shipments.Details;

public class ShipmentDetailsQuery : IRequest<ShipmentResponse>
{
    public int Id { get; set; }

    public class ShipmentDetailsQueryHandler : IRequestHandler<ShipmentDetailsQuery, ShipmentResponse>
    {
        private readonly IShipmentRepository _shipmentRepository;
        private readonly IMapper _mapper;

        public ShipmentDetailsQueryHandler(
        IShipmentRepository shipmentRepository,
        IMapper mapper)
        {
            _shipmentRepository = shipmentRepository;
            _mapper = mapper;
        }

        public async Task<ShipmentResponse> Handle(
            ShipmentDetailsQuery request,
            CancellationToken cancellationToken)
        {
            var shipment = await _shipmentRepository.GetByIdAsync(request.Id, cancellationToken);

            return _mapper.Map<ShipmentResponse>(shipment);
        }
    }
}