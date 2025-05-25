using Common.Domain.Models;

namespace Shipping.Domain.Models.Shipments;

public class ShipmentCompany : Entity
{
    private ShipmentCompany()
    {
    }

    private ShipmentCompany(string name, string code)
    {
        Name = name;
        Code = code;
        Shipments = new HashSet<Shipment>();
    }

    public static ShipmentCompany Create(string name, string code)
        => new ShipmentCompany(name, code);

    public string Name { get; private set; }
    public string Code { get; private set; }
    public virtual ICollection<Shipment> Shipments { get; private set; }
}
