using Common.Domain.ValueObjects;
using Common.Infrastructure.Persistence;
using Shipping.Domain.Models.Shipments;

namespace Shipping.Infrastructure.Persistence;

public class ShippingDbInitializer : DbInitializer
{
    private readonly ShippingDbContext _dbContext;

    public ShippingDbInitializer(ShippingDbContext dbContext)
    : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Initialize()
    {
        if (_dbContext.Shipments.Any())
            return;

        var shipmentCompany1 = new ShipmentCompany("UPS", "https://ups.com");
        var shipmentCompany2 = new ShipmentCompany("FedEx", "https://fedex.com");

        _dbContext.ShipmentCompanies.AddRange(shipmentCompany1, shipmentCompany2);
        _dbContext.SaveChanges();

        var shipment1 = Shipment.Create(
            orderId: OrderId.From(1),
            shippingAddress: new Address("Cicek Sk", "Istanbul", "IST", "Turkiye", "34755"),
            trackingNumber: "TRACK001",
            shipmentCompanyId: shipmentCompany1.Id
        );
        shipment1.AddItem(ProductId.From(1), 2);
        shipment1.AddItem(ProductId.From(2), 1);
        shipment1.Ship();

        var shipment2 = Shipment.Create(
            orderId: OrderId.From(2),
            shippingAddress: new Address("Guzel Sk", "Kocaeli", "KOA", "Turkiye", "41756"),
            trackingNumber: "TRACK002",
            shipmentCompanyId: shipmentCompany2.Id
        );
        shipment2.AddItem(ProductId.From(3), 1);
        shipment2.AddItem(ProductId.From(4), 3);

        _dbContext.Shipments.AddRange(shipment1, shipment2);
        _dbContext.SaveChanges();
    }
}