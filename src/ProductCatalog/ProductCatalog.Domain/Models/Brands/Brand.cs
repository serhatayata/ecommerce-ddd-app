using Common.Domain.Models;
using ProductCatalog.Domain.Models.Products;

namespace ProductCatalog.Domain.Models.Brands;

public class Brand : Entity
{
    private readonly List<Product> _products = new();

    public Brand(
        string name,
        string description,
        string website)
    {
        Name = name;
        Description = description;
        Website = website;
        IsActive = true;
    }

    public string Name { get; private set; }
    public string Description { get; private set; }
    public string Website { get; private set; }
    public bool IsActive { get; private set; }

    public virtual IReadOnlyCollection<Product> Products => _products.AsReadOnly();

    public void Deactivate()
    {
        IsActive = false;
        // AddDomainEvent(new BrandDeactivatedDomainEvent(Id, Name));
    }
}