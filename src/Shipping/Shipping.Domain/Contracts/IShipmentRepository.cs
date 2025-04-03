using Common.Domain.Repositories;
using Shipping.Domain.Models.Shipments;

namespace Shipping.Domain.Contracts;

public interface IShipmentRepository : IRepository<Shipment>
{

}