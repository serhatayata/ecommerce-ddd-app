using Common.Domain.Models;
using ProductCatalog.Domain.Models.Products;

namespace ProductCatalog.Domain.Models.Suppliers;

public class Supplier : Entity
{
    private readonly List<Product> _products = new();

    private Supplier() { }

    private Supplier(
        string name,
        string contactName,
        string email,
        string phone,
        Address address)
    {
        Name = name;
        ContactName = contactName;
        Email = email;
        Phone = phone;
        Address = address;
        IsActive = true;
    }

    public static Supplier Create(
        string name,
        string contactName,
        string email,
        string phone,
        Address address)
        => new Supplier(name, contactName, email, phone, address);

    public string Name { get; private set; }
    public string ContactName { get; private set; }
    public string Email { get; private set; }
    public string Phone { get; private set; }
    public Address Address { get; private set; }
    public bool IsActive { get; private set; }

    public virtual IReadOnlyCollection<Product> Products => _products.AsReadOnly();
}