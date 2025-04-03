using Common.Domain.Models;

namespace Shipping.Domain.Models.Shipments;

public class ShipmentCompany : Entity
{
    public ShipmentCompany()
    {
    }

    public ShipmentCompany(string name, string code)
    {
        Name = name;
        Code = code;
        Shipments = new HashSet<Shipment>();
    }

    public string Name { get; private set; }
    public string Code { get; private set; }
    public virtual ICollection<Shipment> Shipments { get; private set; }
}
