using AutoMapper;
using MediatR;
using Shipping.Application.Queries.ShipmentCompanies.Common;
using Shipping.Domain.Contracts;

namespace Shipping.Application.Queries.ShipmentCompanies.Details;

public class ShipmentCompanyDetailsQuery: IRequest<ShipmentCompanyResponse>
{
    public int Id { get; set; }

    public class ShipmentCompanyDetailsQueryHandler : IRequestHandler<ShipmentCompanyDetailsQuery, ShipmentCompanyResponse>
    {
        private readonly IShipmentRepository _shipmentRepository;
        private readonly IMapper _mapper;
        
        public ShipmentCompanyDetailsQueryHandler(
        IShipmentRepository shipmentRepository,
        IMapper mapper)
        {
            _shipmentRepository = shipmentRepository;
            _mapper = mapper;
        }

        public async Task<ShipmentCompanyResponse> Handle(
        ShipmentCompanyDetailsQuery request,
        CancellationToken cancellationToken)
        {
            var shipmentCompany = await _shipmentRepository.GetShipmentCompanyByIdAsync(request.Id, cancellationToken);
        
            return _mapper.Map<ShipmentCompanyResponse>(shipmentCompany);
        }
    }
}