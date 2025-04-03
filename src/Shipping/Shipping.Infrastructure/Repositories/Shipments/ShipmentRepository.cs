using Common.Infrastructure.Repositories;
using Shipping.Domain.Contracts;
using Shipping.Domain.Models.Shipments;
using Shipping.Infrastructure.Persistence;

namespace Shipping.Infrastructure.Repositories.Shipments;

public class ShipmentRepository : EfRepository<Shipment, ShippingDbContext>, IShipmentRepository
{
    public ShipmentRepository(ShippingDbContext dbContext) 
    : base(dbContext)
    {
    }
}