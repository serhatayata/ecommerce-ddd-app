using MediatR;
using Shipping.Domain.Contracts;
using Shipping.Domain.Models.Shipments;

namespace Shipping.Application.Commands.ShipmentCompanies.Create;

public class CreateShipmentCompanyCommand : IRequest<CreateShipmentCompanyResponse>
{
    public string Name { get; set; }
    public string Code { get; set; }

    public class CreateShipmentCompanyCommandHandler : IRequestHandler<CreateShipmentCompanyCommand, CreateShipmentCompanyResponse>
    {
        private readonly IShipmentRepository _shipmentRepository;

        public CreateShipmentCompanyCommandHandler(
            IShipmentRepository shipmentRepository)
        {
            _shipmentRepository = shipmentRepository;
        }

        public async Task<CreateShipmentCompanyResponse> Handle(
        CreateShipmentCompanyCommand request, 
        CancellationToken cancellationToken)
        {
            var shipmentCompany = new ShipmentCompany(
                request.Name,
                request.Code
            );
            
            await _shipmentRepository.CreateShipmentCompanyAsync(shipmentCompany, cancellationToken);

            return new CreateShipmentCompanyResponse(shipmentCompany.Id);
        }
    }
}