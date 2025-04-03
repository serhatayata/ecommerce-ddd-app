using Common.Application.Models;
using MediatR;
using Shipping.Domain.Contracts;

namespace Shipping.Application.Commands.ShipmentCompanies.Delete;

public class DeleteShipmentCompanyCommand : IRequest<Result>
{
    public int Id { get; set; }

    public class DeleteProductCommandHandler : IRequestHandler<DeleteShipmentCompanyCommand, Result>
    {
        private readonly IShipmentRepository _shipmentRepository;

        public DeleteProductCommandHandler(
        IShipmentRepository shipmentRepository)
        {
            _shipmentRepository = shipmentRepository;
        }

        public async Task<Result> Handle(
        DeleteShipmentCompanyCommand request,
        CancellationToken cancellationToken)
        {
            var shipmentCompany = await _shipmentRepository.GetShipmentCompanyByIdAsync(request.Id, cancellationToken);

            if (shipmentCompany is null)
                return Result.Failure(null);

            await _shipmentRepository.DeleteShipmentCompanyAsync(request.Id, cancellationToken);

            return Result.Success;
        }
    }
}