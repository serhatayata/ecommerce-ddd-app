using Common.Domain.Repositories;
using Common.Domain.ValueObjects;
using Shipping.Domain.Models.Shipments;

namespace Shipping.Domain.Contracts;

public interface IShipmentRepository : IRepository<Shipment>
{
    Task<ShipmentCompany> GetShipmentCompanyByIdAsync(int id, CancellationToken cancellationToken);
    Task<Shipment> GetByOrderIdAsync(OrderId orderId, CancellationToken cancellationToken = default);

    Task<ShipmentCompany> CreateShipmentCompanyAsync(ShipmentCompany shipmentCompany, CancellationToken cancellationToken);
    Task DeleteShipmentCompanyAsync(int id, CancellationToken cancellationToken);
}