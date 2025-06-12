using Common.Domain.ValueObjects;
using Common.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Shipping.Domain.Contracts;
using Shipping.Domain.Models.Shipments;
using Shipping.Infrastructure.Persistence;

namespace Shipping.Infrastructure.Repositories.Shipments;

public class ShipmentRepository : EfRepository<Shipment, ShippingDbContext, int>, IShipmentRepository
{
    private readonly ShippingDbContext _dbContext;

    public ShipmentRepository(ShippingDbContext dbContext) 
    : base(dbContext)
    {
        _dbContext = dbContext;
    }

    #region ShipmentCompany
    public async Task<ShipmentCompany> CreateShipmentCompanyAsync(
    ShipmentCompany shipmentCompany, 
    CancellationToken cancellationToken)
    {
        await _dbContext.ShipmentCompanies.AddAsync(shipmentCompany, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return shipmentCompany;
    }

    public async Task DeleteShipmentCompanyAsync(
    int id, 
    CancellationToken cancellationToken)
    {
        var shipmentCompany = await GetShipmentCompanyByIdAsync(id, cancellationToken);
        if (shipmentCompany == null) return;

        _dbContext.ShipmentCompanies.Remove(shipmentCompany);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task<Shipment> GetByOrderIdAsync(OrderId orderId, CancellationToken cancellationToken = default)
        => _dbContext.Shipments
            .FirstOrDefaultAsync(x => x.OrderId == orderId, cancellationToken);

    public async Task<ShipmentCompany> GetShipmentCompanyByIdAsync(
    int id, 
    CancellationToken cancellationToken)
        => await _dbContext.ShipmentCompanies
            .Include(x => x.Shipments)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    #endregion
}