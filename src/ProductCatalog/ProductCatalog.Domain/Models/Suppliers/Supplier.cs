using Common.Domain.Models;
using ProductCatalog.Domain.Models.Products;

namespace ProductCatalog.Domain.Models.Suppliers;

public class Supplier : Entity
{
    private readonly List<Product> _products = new();

    public Supplier(
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

    public string Name { get; private set; }
    public string ContactName { get; private set; }
    public string Email { get; private set; }
    public string Phone { get; private set; }
    public Address Address { get; private set; }
    public bool IsActive { get; private set; }

    public virtual IReadOnlyCollection<Product> Products => _products.AsReadOnly();

    public void UpdateContact(
    string contactName, 
    string email, 
    string phone)
    {
        ContactName = contactName;
        Email = email;
        Phone = phone;
        // AddDomainEvent(new SupplierContactUpdatedDomainEvent(Id, Name, Email));
    }
}